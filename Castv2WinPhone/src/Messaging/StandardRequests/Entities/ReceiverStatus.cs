using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests.Entities
{
    [DataContract]
    public sealed class ReceiverStatus
    {
        [DataMember(Name = "applications")]
        public ApplicationInfo[] ApplicationsInfo { get; set; }

        [DataMember(Name = "isActiveInput")]
        public bool IsActiveInput { get; set; }

        [DataMember(Name = "volume")]
        public Volume Volume { get; set; }
    }
}
