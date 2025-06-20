using System;

namespace CommonHelpers.Messaging;

public class MessagingCenterQuestion : IMessagingCenterItem
{
    public string Title { get; set; }
        
    public string Message { get; set; }

    public string Okay { get; set; } = "ok";

    public string Cancel { get; set; } = "cancel";
        
    public Action OnOkay { get; set; }

    public Action OnCancel { get; set; }
}