using System.Text.RegularExpressions;
using System;

namespace BotLibrary
{
    public class MessageParser
    {
        public static double price;
        public static bool CheckMessage(string[] message)
        {
            return double.TryParse(message[message.Length - 2], out price)
                    && Regex.IsMatch(message[message.Length - 1], "^рубл.*$");
        }
    }
}
