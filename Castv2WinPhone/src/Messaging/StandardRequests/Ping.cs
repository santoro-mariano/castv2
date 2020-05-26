using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests
{
    [DataContract]
    public sealed class Ping : MessageRequest
    {
        public Ping() : base("PING")
        {
        }
    }
}
