using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests
{
    [DataContract]
    public sealed class Seek : MessageRequestWithId
    {
        public Seek()
            : base("SEEK")
        {
        }

        [DataMember(Name = "currentTime")]
        public int CurrentTime
        {
            get;
            set;
        }

        [DataMember(Name = "resumeState")]
        public string ResumeState
        {
            get;
            set;
        }
    }
}
