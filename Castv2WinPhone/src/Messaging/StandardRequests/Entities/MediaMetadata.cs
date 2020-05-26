using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests.Entities
{
    [DataContract]
    public sealed class MediaMetadata
    {
        [DataMember(Name = "metadataType")]
        public int Type
        {
            get;
            set;
        }

        [DataMember(Name = "title")]
        public string Title
        {
            get;
            set;
        }

        [DataMember(Name = "images")]
        public Image[] Images
        {
            get;
            set;
        }
    }
}
