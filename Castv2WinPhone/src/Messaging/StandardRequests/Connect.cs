using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests
{
    [DataContract]
    public sealed class Connect : MessageRequest
    {
        public Connect() : base("CONNECT")
        {
        }
    }
}
