using System.Runtime.Serialization;
using Castv2WinPhone.Messaging.StandardRequests.Entities;

namespace Castv2WinPhone.Messaging.StandardRequests
{
    [DataContract]
    public sealed class SetVolume : MessageRequestWithId
    {
        public SetVolume() : base("SET_VOLUME")
        {
        }

        [DataMember(Name = "volume")]
        public Volume Volume { get; set; }
    }
}
