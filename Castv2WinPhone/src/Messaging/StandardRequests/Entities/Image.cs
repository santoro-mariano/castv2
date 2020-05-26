using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests.Entities
{
    [DataContract]
    public sealed class Image
    {
        [DataMember(Name = "url")]
        public string Url
        {
            get;
            set;
        }
    }
}
