using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests
{
    [DataContract]
    public sealed class Pause : MessageRequestWithId
    {
        public Pause()
            : base("PAUSE")
        {
        }
    }
}
