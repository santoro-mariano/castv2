using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests
{
    [DataContract]
    public sealed class Close : MessageRequest
    {
        public Close() : base("CLOSE")
        {
        }
    }
}
