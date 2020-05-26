using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests.Entities
{
    [DataContract]
    public sealed class Namespace
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
