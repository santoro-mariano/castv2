using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests.Entities
{
    [DataContract]
    public sealed class MediaStatus
    {
        [DataMember(Name = "mediaSessionId")]
        public int MediaSessionId
        {
            get;
            set;
        }

        [DataMember(Name = "playbackRate")]
        public double PlaybackRate
        {
            get;
            set;
        }

        [DataMember(Name = "supportedMediaCommands")]
        public int SupportedMediaCommands
        {
            get;
            set;
        }

        [DataMember(Name = "volume")]
        public Volume Volume
        {
            get;
            set;
        }

        [DataMember(Name = "playerState")]
        public string PlayerState
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

        [DataMember(Name = "currentTime")]
        public double CurrentTime
        {
            get;
            set;
        }

        [DataMember(Name = "media")]
        public MediaInfo Info
        {
            get;
            set;
        }
    }
}
