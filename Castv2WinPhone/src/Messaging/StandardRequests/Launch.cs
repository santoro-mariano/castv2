using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests
{
    [DataContract]
    public class Launch : MessageRequestWithId
    {
        public Launch() : base("LAUNCH")
        {
        }

        [DataMember(Name = "appId")]
        public string ApplicationId { get; set; }
    }
}
