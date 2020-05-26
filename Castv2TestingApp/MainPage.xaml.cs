using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641
using Castv2WinPhone;
using Windows.UI.Core;
using System.Threading.Tasks;
using Castv2WinPhone.DefaultProtocols;
using Castv2WinPhone.Messaging;
using Castv2WinPhone.Messaging.StandardRequests.Entities;
using DIALClient;
using DIALClient.Model;

namespace Castv2TestingApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.LoadAll();
        }

        private async void LoadAll()
        {
            await CastDiscovery.GetAllDevices();
            await CastDiscovery.LoadApp();
            this.lblLoading.Visibility = Visibility.Collapsed;
            this.spMainPanel.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void btnPlayVideo_Click(object sender, RoutedEventArgs e)
        {
            CastDiscovery.Play();
        }

        private async void btnStopVideo_Click(object sender, RoutedEventArgs e)
        {
            await CastDiscovery.Stop();
        }

        private async void btnPauseVideo_Click(object sender, RoutedEventArgs e)
        {
            await CastDiscovery.Pause();
        }

        private async void btnRewindVideo_Click(object sender, RoutedEventArgs e)
        {
            await CastDiscovery.Rewind();
        }

        private async void btnLoadVideo_Click(object sender, RoutedEventArgs e)
        {
            await CastDiscovery.Load(txtVideoCode.Text);
        }
    }

    public static class CastDiscovery
    {
        private static IDeviceInfo chromecast;
        private static CastClient castClient;

        public static async Task GetAllDevices()
        {
            var devices = await DialServiceDiscovery.GetAllDevices();
            chromecast = devices.Single();
        }

        public static YouTubeProtocol YouTubeProtocol { get; private set; }

        public static async Task LoadApp()
        {
            castClient = new CastClient(chromecast);
            await castClient.Connect();
            //await castClient.ReceiverProtocol.LaunchMediaApp();
            YouTubeProtocol = castClient.GetNewProtocol<YouTubeProtocol>();
            await castClient.ReceiverProtocol.Launch("233637DE");
        }

        public static async Task Load(string videoCode)
        {
            await Stop();
            await YouTubeProtocol.PlayVideo(new VideoData { CurrentTime = 0, VideoId =  videoCode});
            //await castClient.MediaProtocol.Load(new MediaInfo
            //{
            //    Url = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"
            //});
        }

        public static async Task Play()
        {
            await castClient.MediaProtocol.Play(castClient.MediaProtocol.CurrentMediaStatus.Single().MediaSessionId);
        }

        public static async Task Stop()
        {
            await castClient.MediaProtocol.Stop(castClient.MediaProtocol.CurrentMediaStatus.Single().MediaSessionId, true);
        }

        public static async Task Pause()
        {
            await castClient.MediaProtocol.Pause(castClient.MediaProtocol.CurrentMediaStatus.Single().MediaSessionId);
        }

        public static async Task Rewind()
        {
            await castClient.MediaProtocol.Rewind(castClient.MediaProtocol.CurrentMediaStatus.Single().MediaSessionId);
        }
    }
}
