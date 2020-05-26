using System;
using Castv2WinPhone.Messaging.StandardRequests.Entities;

namespace Castv2WinPhone
{
    public sealed class MediaStatusChangedEventArgs : EventArgs
    {
        public MediaStatusChangedEventArgs(MediaStatus[] newStatus)
        {
            this.NewStatus = newStatus;
        }

        public MediaStatus[] NewStatus
        {
            get;
            private set;
        }
    }
}
