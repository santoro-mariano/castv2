using System.Collections.Generic;
using System.Runtime.Serialization;
using Castv2WinPhone.Messaging.StandardRequests.Entities;

namespace Castv2WinPhone.Messaging.StandardRequests
{
    [DataContract]
    public sealed class Load : MessageRequestWithId
    {
        public Load()
            : base("LOAD")
        {
        }

        [DataMember(Name = "media")]
        public MediaInfo Media
        {
            get;
            set;
        }

        [DataMember(Name = "currentTime")]
        public double CurrentTime
        {
            get;
            set;
        }

        [DataMember(Name = "autoplay")]
        public bool AutoPlay
        {
            get;
            set;
        }

        [DataMember(Name = "customData")]
        public Dictionary<string,string> CustomData
        {
            get;
            set;
        }
    }
}
