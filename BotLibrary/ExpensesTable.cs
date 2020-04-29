using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;

namespace BotLibrary
{
    public class ExpensesTable
    {
        private static string path = @"../../../data/tables/";

        public static void CreateOrAppendCSV(string id)
        {
            if (!File.Exists($"{path}{id}.csv"))
            {
                Console.WriteLine($"[CSV] {id} CSV file has been added...");
                //File.WriteAllText($"{path}{id}.csv", "");
            }
            else
                Console.WriteLine($"[CSV] {id} appended his CSV file...");
        }

        public static string ReadCSV(string id)
        {
            //string[] records = File.ReadAllLines($"{path}{id}.csv").Where(e => e.Trim() != "").ToArray();
            //string[][] parsedData = new string[records.Length][];
            //for (int i = 0; i < records.Length; i++)
            //{
            //    parsedData[i] = records[i].Split(',');
            //}

            //for (int i = 0; i < records.Length; i++)
            //{
            //    Console.WriteLine($"{parsedData[0]} {parsedData[1]} {parsedData[2]}");
            //}

            using (var reader = new StreamReader($"{path}{id}.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                var records = csv.GetRecords<PurchaseInfo>().ToList();
                string expenses = $"Ваши покупки: {Environment.NewLine}";
                int temp = 1;
                foreach (var item in records)
                {
                    expenses += $"{temp}. {item.ToString()}" + Environment.NewLine;
                    temp++;
                }

                return expenses;
            }
        }

        public static void WriteCSV(string id, string obj, double price, string currency)
        {
            //CreateOrAppendCSV(id);

            List<PurchaseInfo> records = new List<PurchaseInfo>
            {
                new PurchaseInfo(obj, price, currency),
            };

            // Do not include the header row if the file already exists
            CsvHelper.Configuration.CsvConfiguration csvConfig =
                new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = !File.Exists($"{path}{id}.csv")
                };

            // WARNING: This will throw an error if the file is open in Excel!
            using (FileStream fileStream = new FileStream($"{path}{id}.csv",
                FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    using (var csv = new CsvWriter(streamWriter, csvConfig))
                    {
                        csv.WriteRecords(records);
                    }
                }
            }
        }
    }
}
