using System;
using System.Linq;

namespace Castv2WinPhone
{
    public abstract class Protocol : IDisposable
    {
        private readonly string fixedDestinationId;

        protected Protocol(string protocolNamespace, string fixedDestinationId = null)
        {
            this.Namespace = protocolNamespace;
            this.fixedDestinationId = fixedDestinationId;
        }

        ~Protocol()
        {
            if (!this.IsDisposed)
            {
                this.Dispose(false);
            }
        }

        protected CastClient SubscribedClient
        {
            get;
            private set;
        }

        public string Namespace
        {
            get;
            private set;
        }

        public bool IsDisposed
        {
            get;
            private set;
        }

        public string CurrentDestinationId
        {
            get
            {
                if (!string.IsNullOrEmpty(fixedDestinationId))
                {
                    return fixedDestinationId;
                }

                try
                {
                    if (this.SubscribedClient != null && this.SubscribedClient.ReceiverProtocol.CurrentReceiverStatus != null)
                    {
                        var appInfo = this.SubscribedClient.ReceiverProtocol.CurrentReceiverStatus.ApplicationsInfo.SingleOrDefault(i => i.SupportedNamespaces.Any(ns => ns.Name.Equals(this.Namespace)));
                        if (appInfo != null)
                        {
                            return appInfo.TransportId;
                        }
                    }
                }
                catch
                {}
                return CastConstants.DestinationId;
            }
        }

        public string CurrentSessionId
        {
            get
            {
                try
                {
                    if (this.SubscribedClient != null && this.SubscribedClient.ReceiverProtocol.CurrentReceiverStatus != null)
                    {
                        var appInfo = this.SubscribedClient.ReceiverProtocol.CurrentReceiverStatus.ApplicationsInfo.SingleOrDefault(i => i.SupportedNamespaces.Any(ns => ns.Name.Equals(this.Namespace)));
                        if (appInfo != null)
                        {
                            return appInfo.SessionId;
                        }
                    }
                }
                catch
                { }
                return null;
            }
        }

        public virtual void SubscribeToClient(CastClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }

            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            if (client.IsDisposed)
            {
                throw new ObjectDisposedException(client.GetType().Name);
            }

            if (this.SubscribedClient != null)
            {
                this.RemoveClientSubscription();
            }
            this.SubscribedClient = client;
        }

        public virtual void RemoveClientSubscription()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            this.SubscribedClient = null;
        }

        public void Dispose()
        {
            if (!this.IsDisposed)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        private void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            this.RemoveClientSubscription();

            this.IsDisposed = true;
        }
    }
}
