using System;
using Castv2WinPhone.Messaging;

namespace Castv2WinPhone
{
    //TODO: Agregar posibilidad de esperar respuesta de un mensaje
    public abstract class ProtocolWithMessageListener : Protocol
    {
        private readonly object syncRoot = new object();

        protected ProtocolWithMessageListener(string protocolNamespace) : base(protocolNamespace)
        {
        }

        public override void SubscribeToClient(CastClient client)
        {
            base.SubscribeToClient(client);
            this.SubscribedClient.MessageReceived += this.OnMessageReceivedHandler;
        }

        public override void RemoveClientSubscription()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            if (this.SubscribedClient == null)
            {
                return;
            }

            this.SubscribedClient.MessageReceived -= this.OnMessageReceivedHandler;
            base.RemoveClientSubscription();
        }

        private void OnMessageReceivedHandler(object sender, MessageReceivedEventArgs e)
        {
            if (e.Message.Namespace.Equals(this.Namespace))
            {
                lock (syncRoot)
                {
                    this.OnMessageReceived(e.Data, e.Message);
                }
            }
        }

        protected abstract void OnMessageReceived(byte[] data, CastMessage message);
    }
}
