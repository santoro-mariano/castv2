using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests
{
    [DataContract]
    public sealed class GetStatus : MessageRequestWithId
    {
        public GetStatus() : base("GET_STATUS")
        {
        }
    }
}
