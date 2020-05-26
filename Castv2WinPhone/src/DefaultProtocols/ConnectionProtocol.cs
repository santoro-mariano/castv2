using System.Threading.Tasks;
using Castv2WinPhone.Messaging;
using Castv2WinPhone.Messaging.StandardRequests;

namespace Castv2WinPhone.DefaultProtocols
{
    public sealed class ConnectionProtocol : Protocol
    {
        private readonly CastMessage closeMessage, connectMessage;

        public ConnectionProtocol(string destinationId = CastConstants.DestinationId)
            : base("urn:x-cast:com.google.cast.tp.connection", destinationId)
        {
            closeMessage = MessageFactory.Create<Close>(this);
            connectMessage = MessageFactory.Create<Connect>(this);
        }

        public bool IsConnected
        {
            get;
            private set;
        }

        public async Task Connect()
        {
            await this.SubscribedClient.SendMessage(connectMessage);
            this.IsConnected = true;
        }

        public async Task Close()
        {
            await this.SubscribedClient.SendMessage(closeMessage);
        }
    }
}
