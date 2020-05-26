using System;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Security.Cryptography.Certificates;
using Windows.Storage.Streams;

namespace Castv2WinPhone
{
    internal sealed class SocketClient : IDisposable
    {
        private readonly HostName deviceHostName;
        private DataReader streamReader;
        private DataWriter streamWriter;
        private StreamSocket socket;

        public SocketClient(HostName deviceHostName)
        {
            this.deviceHostName = deviceHostName;
        }

        ~SocketClient()
        {
            if (!this.IsDisposed)
            {
                this.Dispose(false);
            }
        }

        public bool IsConnected
        {
            get;
            private set;
        }

        public bool IsDisposed
        {
            get;
            private set;
        }

        public async Task Connect()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            if (this.IsConnected)
            {
                return;
            }

            try
            {
                socket = new StreamSocket();
                socket.Control.OutboundBufferSizeInBytes = 2048;
                socket.Control.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
                socket.Control.IgnorableServerCertificateErrors.Add(ChainValidationResult.InvalidName);
                await socket.ConnectAsync(deviceHostName, CastConstants.ConnectionPort, SocketProtectionLevel.Tls12);
                this.streamReader = new DataReader(socket.InputStream) {InputStreamOptions = InputStreamOptions.Partial};
                this.streamWriter = new DataWriter(socket.OutputStream);
                this.ListenPackets();
                this.IsConnected = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public void Disconnect()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            if (!this.IsConnected)
            {
                return;
            }

            this.streamReader.Dispose();
            this.streamReader = null;
            this.streamWriter.Dispose();
            this.streamWriter = null;
            this.socket = null;
            this.IsConnected = false;
        }

        public async Task Write(byte[] data)
        {
            streamWriter.WriteBytes(data);
            await streamWriter.StoreAsync();
        }

        private void ListenPackets()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await this.ReadPacket();
                }
            });
        }

        private async Task ReadPacket()
        {
            try
            {
                await streamReader.LoadAsync(2048);
                var buffer = streamReader.ReadBuffer(streamReader.UnconsumedBufferLength);
                if (buffer.Length == 4)
                {
                    var header = buffer.ToArray();
                    await streamReader.LoadAsync(2048);
                    buffer = streamReader.ReadBuffer(streamReader.UnconsumedBufferLength);
                    if (buffer.Length > 4)
                    {
                        var data = buffer.ToArray();
                        var message = new byte[header.Length + data.Length];
                        header.CopyTo(message, 0);
                        data.CopyTo(message, header.Length);
                        RaisePacketReceived(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }

        public event EventHandler<PacketReceivedEventArgs> PacketReceived;

        private void RaisePacketReceived(byte[] data)
        {
            if (PacketReceived != null)
            {
                this.PacketReceived(this, new PacketReceivedEventArgs(data));
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

        private void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            this.Disconnect();

            this.IsDisposed = true;
        }
    }
}
