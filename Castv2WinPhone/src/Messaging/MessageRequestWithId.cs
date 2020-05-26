using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging
{
    [DataContract]
    public abstract class MessageRequestWithId : MessageRequest
    {
        private static int lastRequestId = 0;

        protected MessageRequestWithId(string messageType) : base(messageType)
        {
            this.RequestId = GetNextId();
        }

        [DataMember(Name = "requestId")]
        public int RequestId { get; set; }

        public static int GetNextId()
        {
            return lastRequestId++;
        }
    }
}
