using System;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Helpers;

namespace BotLibrary
{
    public class CurrencyParser
    {
        protected static HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://currate.ru/api/?get=rates&pairs=USDRUB&key=a0fe46f72468974ec777ef0718173d88");
        public HttpWebRequest MyReq
        {
            get
            {
                return myReq;
            }
        }
        public static string getCurrency()
        {
            string parsed = String.Empty;
            HttpWebResponse response = (HttpWebResponse)myReq.GetResponse();
            using (Stream receiveStream = response.GetResponseStream())
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    parsed = readStream.ReadToEnd();
                }
            }
            dynamic data = Json.Decode(parsed);
            return (data.data.USDRUB);
        }
    }
}
