using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking;
using Castv2WinPhone.DefaultProtocols;
using Castv2WinPhone.Messaging;
using DIALClient.Model;
using ProtoBuf;

namespace Castv2WinPhone
{
    public sealed class CastClient : IDisposable
    {
        private readonly ConnectionProtocol connectionProtocol = new ConnectionProtocol();
        private readonly HeartbeatProtocol heartbeatProtocol = new HeartbeatProtocol();
        private readonly SocketClient client;

        public CastClient(IDeviceInfo deviceInfo)
        {
            this.DeviceInfo = deviceInfo;
            client = new SocketClient(new HostName(this.DeviceInfo.UrlBase.Host));
            client.PacketReceived += this.OnPacketReceived;
            connectionProtocol.SubscribeToClient(this);
            heartbeatProtocol.SubscribeToClient(this);
            this.ReceiverProtocol = this.GetNewProtocol<ReceiverProtocol>();
            this.MediaProtocol = this.GetNewProtocol<MediaProtocol>();
        }

        ~CastClient()
        {
            if (!this.IsDisposed)
            {
                this.Dispose(false);
            }
        }

        public IDeviceInfo DeviceInfo { get; private set; }

        public bool IsDisposed { get; private set; }

        public ReceiverProtocol ReceiverProtocol
        {
            get;
            private set;
        }

        public MediaProtocol MediaProtocol
        {
            get;
            private set;
        }

        public bool IsReady
        {
            get
            {
                return this.client.IsConnected && this.connectionProtocol.IsConnected && this.ReceiverProtocol.CurrentReceiverStatus != null;
            }
        }
        
        internal event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public async Task Connect()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            if (this.connectionProtocol.IsConnected)
            {
                return;
            }

            await this.client.Connect();
            await this.connectionProtocol.Connect();
            this.heartbeatProtocol.StartHeartbeat();
            await this.ReceiverProtocol.GetStatus();
        }

        public async Task Disconnect()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            this.heartbeatProtocol.StopHeartbeat();
            await this.connectionProtocol.Close();
            this.client.Disconnect();
        }

        public async Task SendMessage(CastMessage message, bool includeHeader = true)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            if (!this.client.IsConnected)
            {
                throw new InvalidOperationException("CastClient must be connected to device before call this method.");
            }

#if DEBUG
            Debug.WriteLine(string.Format("OUT: {0}", message.PayloadUtf8));
#endif
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, message);
                var binaryMessage = stream.ToArray();
                if (includeHeader)
                {
                    var header = BitConverter.GetBytes((UInt32)binaryMessage.Length).Reverse().ToArray();
                    var allData = new byte[header.Length + binaryMessage.Length];
                    header.CopyTo(allData, 0);
                    binaryMessage.CopyTo(allData, header.Length);
                    await client.Write(allData);
                }
                else
                {
                    await client.Write(binaryMessage);
                }
            }
        }

        public void Dispose()
        {
            if (!this.IsDisposed)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        private void OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            using (var stream = new MemoryStream(e.Data, false))
            {
                var message = Serializer.DeserializeWithLengthPrefix<CastMessage>(stream, PrefixStyle.Fixed32BigEndian);
#if DEBUG
                Debug.WriteLine(string.Format("IN: {0}", message.PayloadUtf8));
#endif
                if (this.MessageReceived != null)
                {
                    this.MessageReceived(this, new MessageReceivedEventArgs(e.Data, message));
                }
            }
        }

        private void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            this.connectionProtocol.Dispose();
            this.heartbeatProtocol.Dispose();
            this.ReceiverProtocol.Dispose();
            this.MediaProtocol.Dispose();
            this.client.PacketReceived -= this.OnPacketReceived;
            this.client.Dispose();

            this.IsDisposed = true;
        }
    }
}
