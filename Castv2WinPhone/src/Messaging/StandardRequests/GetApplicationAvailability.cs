using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests
{
    [DataContract]
    public sealed class GetApplicationAvailability : MessageRequestWithId
    {
        public GetApplicationAvailability() : base("GET_APP_AVAILABILITY")
        {
        }

        [DataMember(Name = "appId")]
        public string[] ApplicationsIds { get; set; }
    }
}
