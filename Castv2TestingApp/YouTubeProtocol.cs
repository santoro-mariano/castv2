using System.Linq;
using System.Threading.Tasks;
using Castv2WinPhone;
using Castv2WinPhone.Messaging;

namespace Castv2TestingApp
{
    public sealed class YouTubeProtocol : Protocol
    {
        public YouTubeProtocol()
            : base("urn:x-cast:com.google.youtube.mdx")
        {
        }

        public async Task PlayVideo(VideoData data)
        {
            var msg = MessageFactory.Create(this,new FlingVideoRequest{VideoData = data});
            msg.DestinationId = this.SubscribedClient.ReceiverProtocol.CurrentReceiverStatus.ApplicationsInfo.First().TransportId;
            await this.SubscribedClient.SendMessage(msg);
        }
    }
}
