using System;
using Newtonsoft.Json;

namespace Castv2WinPhone.Messaging
{
    public sealed class CastMessage<T> : ICastMessageInfo where T : MessageRequest
    {
        private static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        public CastMessage()
        {
        }

        public CastMessage(ICastMessageInfo message)
        {
            this.ProtocolVersion = message.ProtocolVersion;
            this.SourceId = message.SourceId;
            this.DestinationId = message.DestinationId;
            this.Namespace = message.Namespace;
            this.PayloadType = message.PayloadType;
        }

        public ProtocolVersion ProtocolVersion { get; set; }

        public string SourceId { get; set; }

        public string DestinationId { get; set; }

        public string Namespace { get; set; }

        public PayloadType PayloadType { get; set; }

        public T Payload { get; set; }

        public static implicit operator CastMessage<T>(CastMessage message)
        {
            var msg = new CastMessage<T>(message);
            if (message.PayloadType == PayloadType.Binary)
            {
                throw new NotSupportedException("CastMessages with binary payload does not supported in the current version");
            }
            if (!string.IsNullOrEmpty(message.PayloadUtf8))
            {
                msg.Payload = JsonConvert.DeserializeObject<T>(message.PayloadUtf8); 
            }
            return msg;
        }

        public static implicit operator CastMessage(CastMessage<T> message)
        {
            var msg = new CastMessage(message);
            if (message.PayloadType == PayloadType.Binary)
            {
                throw new NotSupportedException("CastMessages with binary payload does not supported in the current version");
            }
            if (message.Payload != null)
            {
                msg.PayloadUtf8 = JsonConvert.SerializeObject(message.Payload, serializerSettings);
            }
            return msg;
        }
    }
}
