using System.Runtime.Serialization;
using Castv2WinPhone.Messaging;

namespace Castv2TestingApp
{
    [DataContract]
    public class FlingVideoRequest : MessageRequestWithId
    {
        public FlingVideoRequest()
            : base("flingVideo")
        {
        }

        [DataMember(Name = "data")]
        public VideoData VideoData { get; set; }
    }
}
