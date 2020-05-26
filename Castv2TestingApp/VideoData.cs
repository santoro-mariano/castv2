using System.Runtime.Serialization;

namespace Castv2TestingApp
{
    [DataContract]
    public class VideoData
    {
        [DataMember(Name = "currentTime")]
        public int CurrentTime { get; set; }

        [DataMember(Name = "videoId")]
        public string VideoId { get; set; }
    }
}
