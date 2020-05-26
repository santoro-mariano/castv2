using Castv2WinPhone.Messaging;

namespace Castv2TestingApp
{
    internal sealed class GetMdxSessionStatusRequest : MessageRequestWithId
    {
        public GetMdxSessionStatusRequest()
            : base("getMdxSessionStatus")
        {
        }
    }
}
