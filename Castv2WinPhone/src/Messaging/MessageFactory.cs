using System;

namespace Castv2WinPhone.Messaging
{
    public static class MessageFactory
    {
        public static CastMessage<T> Create<T>(Protocol protocolInstance, T messageRequest = null, bool incluseSessionId = false, int? mediaSessionId = null) where T : MessageRequest
        {
            var payload = messageRequest ?? Activator.CreateInstance<T>();
            if (incluseSessionId)
            {
                payload.SessionId = protocolInstance.CurrentSessionId;
            }
            payload.MediaSessionId = mediaSessionId;
            var message = new CastMessage<T>
            {
                DestinationId = protocolInstance.CurrentDestinationId,
                SourceId = CastConstants.SourceId,
                Namespace = protocolInstance.Namespace,
                Payload = payload
            };
            return message;
        }
    }
}
