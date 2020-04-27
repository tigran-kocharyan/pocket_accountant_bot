using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLibrary
{
    public class PurchaseInfo
    {
        public string Obj { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }

        public PurchaseInfo(string obj, double price, string currency)
        {
            Obj = obj;
            Price = price;
            Currency = currency;
        }

        public override string ToString()
        {
            return $"{Obj} {Price} {Currency}";
        }
    }
}
