using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrepperLib.Utility
{
    public static class Message
    {
        public static IList<string> MessageList { get; private set; }

        public static void Add(string message)
        {
            if (MessageList.Count < 1) MessageList = new List<string>();
            if (!MessageList.Contains(message)) MessageList.Add(message);
        }

        public static void Clear()
        {
            MessageList = new List<string>();
        }
    }
}
