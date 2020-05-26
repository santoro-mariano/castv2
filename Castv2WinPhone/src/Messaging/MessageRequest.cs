using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging
{
    [DataContract]
    public abstract class MessageRequest
    {
        protected MessageRequest(string messageType)
        {
            this.Type = messageType;
        }

        [DataMember(Name = "type")]
        public string Type { get; private set; }

        [DataMember(Name = "sessionId")]
        public string SessionId
        {
            get;
            set;
        }

        [DataMember(Name = "mediaSessionId")]
        public int? MediaSessionId
        {
            get;
            set;
        }
    }
}
