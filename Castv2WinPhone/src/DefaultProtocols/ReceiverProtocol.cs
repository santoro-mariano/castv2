using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Castv2WinPhone.Messaging;
using Castv2WinPhone.Messaging.StandardRequests;
using Castv2WinPhone.Messaging.StandardRequests.Entities;
using ReceiverStatus = Castv2WinPhone.Messaging.StandardRequests.Entities.ReceiverStatus;

namespace Castv2WinPhone.DefaultProtocols
{
    public sealed class ReceiverProtocol : ProtocolWithMessageListener
    {
        private const string MediaAppId = "CC1AD845";
        private int launchRequestId = -1;
        private Barrier autoStartSessionBarrier;

        public ReceiverProtocol() : base("urn:x-cast:com.google.cast.receiver")
        {}

        public ReceiverStatus CurrentReceiverStatus { get; private set; }

        public async Task Launch(string applicationId, bool autoStartSession = true)
        {
            launchRequestId = -1;
            autoStartSessionBarrier = new Barrier(2);
            var launchMessage = MessageFactory.Create(this,new Launch {ApplicationId = applicationId});
            if (autoStartSession)
            {
                launchRequestId = launchMessage.Payload.RequestId;
            }
            await this.SubscribedClient.SendMessage(launchMessage);
            if (autoStartSession)
            {
                autoStartSessionBarrier.SignalAndWait();
                launchRequestId = -1;
                var appInfo = this.CurrentReceiverStatus.ApplicationsInfo.SingleOrDefault(i => i.Id == applicationId);
                if (appInfo != null)
                {
                    var cp = this.SubscribedClient.GetNewProtocol<ConnectionProtocol>(appInfo.TransportId);
                    await cp.Connect();
                }
            }
        }

        public async Task LaunchMediaApp()
        {
            await this.Launch(MediaAppId);
        }

        public async Task Stop(string sessionId)
        {
            var stopMessage = MessageFactory.Create(this, new Stop {SessionId = sessionId});
            await this.SubscribedClient.SendMessage(stopMessage);
        }

        public async Task GetStatus()
        {
            var getStatusMessage = MessageFactory.Create<GetStatus>(this);
            await this.SubscribedClient.SendMessage(getStatusMessage);
        }

        public async Task GetAppAvailability(string[] applicationsIds)
        {
            var getAppAvailabilityMessage = MessageFactory.Create(this, new GetApplicationAvailability {ApplicationsIds = applicationsIds});
            await this.SubscribedClient.SendMessage(getAppAvailabilityMessage);
        }

        public async Task SetVolume(Volume volume)
        {
            var setVolumeMessage = MessageFactory.Create(this, new SetVolume {Volume = volume});
            await this.SubscribedClient.SendMessage(setVolumeMessage);
        }

        public event EventHandler<ReceiverStatusChangedEventArgs> ReceiverStatusChanged;

        protected override async void OnMessageReceived(byte[] data, CastMessage message)
        {
            if (message.PayloadUtf8.Contains("RECEIVER_STATUS"))
            {
                var receiverStatusMessage = (CastMessage<Messaging.StandardRequests.ReceiverStatus>) message;
                this.CurrentReceiverStatus = receiverStatusMessage.Payload.Status;
                if (this.launchRequestId == receiverStatusMessage.Payload.RequestId)
                {
                    this.autoStartSessionBarrier.SignalAndWait(500);
                }
                if (this.ReceiverStatusChanged != null)
                {
                    this.ReceiverStatusChanged(this, new ReceiverStatusChangedEventArgs(this.CurrentReceiverStatus));
                }
            }
        }
    }
}
