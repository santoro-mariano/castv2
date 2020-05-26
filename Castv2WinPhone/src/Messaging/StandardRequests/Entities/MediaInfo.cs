using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests.Entities
{
    [DataContract]
    public class MediaInfo
    {
        public MediaInfo()
        {
            this.StreamType = "BUFFERED";
        }

        [DataMember(Name = "contentId")]
        public string Url
        {
            get;
            set;
        }

        [DataMember(Name = "streamType")]
        public string StreamType
        {
            get;
            set;
        }

        [DataMember(Name = "contentType")]
        public string ContentType
        {
            get;
            set;
        }

        [DataMember(Name = "metadata")]
        public MediaMetadata Metadata
        {
            get;
            set;
        }

        [DataMember(Name = "customData")]
        public Dictionary<string, string> CustomData
        {
            get;
            set;
        }

        [DataMember(Name = "duration")]
        public int Duration
        {
            get;
            set;
        }
    }
}
