using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Castv2WinPhone.Messaging;
using Castv2WinPhone.Messaging.StandardRequests;

namespace Castv2WinPhone.DefaultProtocols
{
    internal sealed class HeartbeatProtocol : Protocol
    {
        private readonly CastMessage pingMessage;
        private CancellationTokenSource cancellationTokenSource;

        public HeartbeatProtocol() : base("urn:x-cast:com.google.cast.tp.heartbeat", CastConstants.DestinationId)
        {
            this.HeartbeatIntervalMilliseconds = 5000;
            pingMessage = MessageFactory.Create<Ping>(this);
        }

        public int HeartbeatIntervalMilliseconds
        {
            get;
            set;
        }

        public bool IsBeating
        {
            get;
            private set;
        }

        public void StartHeartbeat()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            if (this.IsBeating)
            {
                return;
            }

            this.IsBeating = true;
            cancellationTokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(async () =>
            {
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        await this.SubscribedClient.SendMessage(pingMessage);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                    await Task.Delay(this.HeartbeatIntervalMilliseconds);
                }
            }, cancellationTokenSource.Token);
        }

        public void StopHeartbeat()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            if (!this.IsBeating)
            {
                return;
            }

            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            this.IsBeating = false;
        }
    }
}
