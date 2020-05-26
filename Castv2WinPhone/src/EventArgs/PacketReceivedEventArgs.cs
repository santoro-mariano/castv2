using System;
namespace Castv2WinPhone
{
    internal sealed class PacketReceivedEventArgs : EventArgs
    {
        public PacketReceivedEventArgs(byte[] data)
        {
            this.Data = data;
        }

        public byte[] Data { get; private set; }
    }
}
