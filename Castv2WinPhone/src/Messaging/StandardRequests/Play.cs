using System.Runtime.Serialization;

namespace Castv2WinPhone.Messaging.StandardRequests
{
    [DataContract]
    public sealed class Play : MessageRequestWithId
    {
        public Play()
            : base("PLAY")
        {
        }
    }
}
