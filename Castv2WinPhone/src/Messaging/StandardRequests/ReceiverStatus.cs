using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests
{
    [DataContract]
    public sealed class ReceiverStatus : MessageRequestWithId
    {
        public ReceiverStatus() : base("RECEIVER_STATUS")
        {
        }

        [DataMember(Name = "status")]
        public Entities.ReceiverStatus Status { get; set; }
    }
}
