using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Castv2WinPhone.Messaging;
using Castv2WinPhone.Messaging.StandardRequests;
using Castv2WinPhone.Messaging.StandardRequests.Entities;
using MediaStatus = Castv2WinPhone.Messaging.StandardRequests.Entities.MediaStatus;

namespace Castv2WinPhone.DefaultProtocols
{
    public sealed class MediaProtocol : ProtocolWithMessageListener
    {
        private readonly Dictionary<int, Barrier> barriers = new Dictionary<int, Barrier>();

        public MediaProtocol()
            : base("urn:x-cast:com.google.cast.media")
        {
        }

        public MediaStatus[] CurrentMediaStatus
        {
            get;
            set;
        }

        public async Task Load(MediaInfo info, Dictionary<string, string> customData = null, bool waitResponse = false, double currentTime = 0, bool autoPlay = true)
        {
            var msg = MessageFactory.Create(this, new Load
            {
                Media = info, CurrentTime = currentTime, AutoPlay = autoPlay, CustomData = customData
            },true);

            if (waitResponse)
            {
                await this.SendMessageAndWaitResponse(msg);
            }
            else
            {
                await this.SubscribedClient.SendMessage(msg);
            }
            
        }

        public async Task Play(int? mediaSessionId = null, bool waitResponse = false)
        {
            var msg = MessageFactory.Create<Play>(this,null, true, mediaSessionId);
            if (waitResponse)
            {
                await this.SendMessageAndWaitResponse(msg);
            }
            else
            {
                await this.SubscribedClient.SendMessage(msg);
            }
            
        }

        public async Task Pause(int? mediaSessionId = null, bool waitResponse = false)
        {
            var msg = MessageFactory.Create<Pause>(this,null, true, mediaSessionId);
            if (waitResponse)
            {
                await this.SendMessageAndWaitResponse(msg);
            }
            else
            {
                await this.SubscribedClient.SendMessage(msg);
            }
            
        }

        public async Task Stop(int? mediaSessionId = null, bool waitResponse = false)
        {
            var msg = MessageFactory.Create<Stop>(this, null, true, mediaSessionId);
            if (waitResponse)
            {
                await this.SendMessageAndWaitResponse(msg);
            }
            else
            {
                await this.SubscribedClient.SendMessage(msg);
            }
            
        }

        public async Task Rewind(int? mediaSessionId = null, bool waitResponse = false)
        {
            await this.Seek(0, mediaSessionId, waitResponse);
        }

        public async Task Seek(int currentTime, int? mediaSessionId = null, bool waitResponse = false, string resumeState = "PLAYBACK_START")
        {
            var msg = MessageFactory.Create(this, new Seek
            {
                CurrentTime = currentTime, ResumeState = resumeState
            },true, mediaSessionId);

            if (waitResponse)
            {
                await this.SendMessageAndWaitResponse(msg);
            }
            else
            {
                await this.SubscribedClient.SendMessage(msg);
            }
        }

        private async Task SendMessageAndWaitResponse<T>(CastMessage<T> message, int millisecondsTimeout = 5000, bool includeHeader = true) where T : MessageRequestWithId
        {
            this.barriers.Add(message.Payload.RequestId, new Barrier(2));
            await this.SubscribedClient.SendMessage(message, includeHeader);
            this.barriers[message.Payload.RequestId].SignalAndWait(millisecondsTimeout);
            this.barriers.Remove(message.Payload.RequestId);
        }

        public async Task GetStatus(bool waitResponse = false)
        {
            var msg = MessageFactory.Create<GetStatus>(this,null, true);
            if (waitResponse)
            {
                await this.SendMessageAndWaitResponse(msg);
            }
            else
            {
                await this.SubscribedClient.SendMessage(msg);
            }
        }

        public event EventHandler<MediaStatusChangedEventArgs> MediaStatusChanged; 

        protected override void OnMessageReceived(byte[] data, CastMessage message)
        {
            if (message.PayloadUtf8.Contains("MEDIA_STATUS"))
            {
                var mediaStatusRequest = ((CastMessage<Messaging.StandardRequests.MediaStatus>) message).Payload;
                this.CurrentMediaStatus = mediaStatusRequest.Status;
                if (barriers.ContainsKey(mediaStatusRequest.RequestId))
                {
                    barriers[mediaStatusRequest.RequestId].SignalAndWait(1000);
                }
                if (this.MediaStatusChanged != null)
                {
                    this.MediaStatusChanged(this, new MediaStatusChangedEventArgs(this.CurrentMediaStatus));
                }
            }
        }
    }
}
