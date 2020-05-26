using System;
using Castv2WinPhone.Messaging.StandardRequests.Entities;

namespace Castv2WinPhone
{
    public sealed class ReceiverStatusChangedEventArgs : EventArgs
    {
        public ReceiverStatusChangedEventArgs(ReceiverStatus newStatus)
        {
            this.NewStatus = newStatus;
        }

        public ReceiverStatus NewStatus
        {
            get;
            private set;
        }
    }
}
