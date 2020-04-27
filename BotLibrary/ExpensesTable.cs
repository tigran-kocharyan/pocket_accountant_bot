using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BotLibrary
{
    public class ExpensesTable
    {
        private static string path = @"../../../data/tables/";
        public static void AppendCSV(long id)
        {
            CreateCSV(id.ToString());
        }

        public static void CreateCSV(string id)
        {
            if (!File.Exists($"{path}{id}.csv"))
            {
                Console.WriteLine($"[CSV] {id} CSV file has been added...");
                File.Create($"{path}{id}.csv");
            }
            else
                Console.WriteLine($"[CSV] {id} appended his CSV file...");
        }
    }
}
