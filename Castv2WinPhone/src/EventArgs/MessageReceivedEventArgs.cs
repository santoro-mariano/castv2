using System;
using Castv2WinPhone.Messaging;

namespace Castv2WinPhone
{
    internal sealed class MessageReceivedEventArgs : EventArgs
    {

        public MessageReceivedEventArgs(byte[] data, CastMessage message)
        {
            this.Data = data;
            this.Message = message;
        }

        public byte[] Data
        {
            get;
            private set;
        }

        public CastMessage Message
        {
            get;
            private set;
        }
    }
}
