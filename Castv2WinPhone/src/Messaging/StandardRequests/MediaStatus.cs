using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests
{
    [DataContract]
    public sealed class MediaStatus : MessageRequestWithId
    {
        public MediaStatus()
            : base("MEDIA_STATUS")
        {
        }

        [DataMember(Name = "status")]
        public Entities.MediaStatus[] Status
        {
            get;
            set;
        }
    }
}
