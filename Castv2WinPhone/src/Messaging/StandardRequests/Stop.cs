using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests
{
    [DataContract]
    public sealed class Stop : MessageRequestWithId
    {
        public Stop() : base("STOP")
        {
        }
    }
}
