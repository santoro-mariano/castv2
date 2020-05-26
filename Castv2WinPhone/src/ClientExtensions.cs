using System;

namespace Castv2WinPhone
{
    public static class ClientExtensions
    {
        public static T GetNewProtocol<T>(this CastClient client, params object[] args) where T: Protocol
        {
            var protocol = (T)Activator.CreateInstance(typeof (T), args);
            protocol.SubscribeToClient(client);
            return protocol;
        }
    }
}
