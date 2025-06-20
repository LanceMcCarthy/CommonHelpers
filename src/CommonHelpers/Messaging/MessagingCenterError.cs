using System;

namespace CommonHelpers.Messaging;

public class MessagingCenterError : IMessagingCenterItem
{
    public string Caller { get; set; }
        
    public Exception Exception { get; set; }
}