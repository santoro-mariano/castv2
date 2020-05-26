using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests.Entities
{
    [DataContract]
    public sealed class Volume
    {
        [DataMember(Name = "level")]
        public float? Level { get; set; }

        [DataMember(Name = "muted")]
        public bool? Muted { get; set; }
    }
}
