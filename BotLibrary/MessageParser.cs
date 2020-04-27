using System.Text.RegularExpressions;
using System;

namespace BotLibrary
{
    public class MessageParser
    {
        public static bool CheckMessage(string[] text)
        {
            int length = text.Length;
            return Regex.IsMatch(text[length - 1], "^рубл.*$");
        }
    }
}
