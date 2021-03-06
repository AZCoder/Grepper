﻿using System.Collections.Generic;

namespace GrepperLib.Utility
{
    public static class Message
    {
        public static IList<string> MessageList { get; private set; }

        public static void Add(string message)
        {
            if ((MessageList == null) || (MessageList.Count < 1)) Clear();
            if (MessageList != null && !MessageList.Contains(message)) MessageList.Add(message);
        }

        public static void Clear()
        {
            MessageList = new List<string>();
        }

        public enum MessageStatus
        {
            Success,
            Warning,
            Error
        }
    }
}
