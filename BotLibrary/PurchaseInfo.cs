﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace BotLibrary
{
    /// <summary>
    /// Класс, который хранит все методы и поля для хранения информации
    /// о покупках.
    /// </summary>
    public class PurchaseInfo
    {
        // Путь хранения информации о покупках и поля для заполнения.
        private static string path = @"../../../data/purchases/";
        private DateTime date;
        public string Obj { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                if (value == default)
                    date = DateTime.Now.Date;
                else
                    date = value;

            }
        }

        /// <summary>
        /// Конструктор для создания объектов класса PurchaseInfo.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="price"></param>
        /// <param name="currency"></param>
        /// <param name="type"></param>
        /// <param name="date"></param>
        public PurchaseInfo(string obj, double price, string currency,
          string type, DateTime date)
        {
            Obj = obj;
            Price = price;
            Currency = currency;
            Date = date;
            Type = type;
        }

        /// <summary>
        /// Метод для записи покупок в файл JSON.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="purchase"></param>
        public static void WritePurchase(long id, PurchaseInfo purchase)
        {
            List<PurchaseInfo> purchases = new List<PurchaseInfo>();
            if (File.Exists($"{path}{id}.json"))
            {
                purchases = ReadPurchase(id);
                purchases.Add(purchase);
            }
            else
            {
                purchases.Add(purchase);
            }
            using (JsonWriter fs = new JsonTextWriter(new StreamWriter($"{path}{id}.json")))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(fs, purchases);
            }
        }

        /// <summary>
        /// Метод для чтения информации о покупках из файла JSON.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<PurchaseInfo> ReadPurchase(long id)
        {
            using (JsonReader fs = new JsonTextReader(new StreamReader($"{path}{id}.json")))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                List<PurchaseInfo> purchases =
                    jsonSerializer.Deserialize(fs, typeof(List<PurchaseInfo>)) as List<PurchaseInfo>;
                return purchases;
            }
        }

        /// <summary>
        /// Метод, который с помощью StringBuilder объединяет
        /// информацию о всех покупках из полученного списка и
        /// возвращает в виде строки.
        /// </summary>
        /// <param name="purchases"></param>
        /// <returns></returns>
        public static string PurchasesToString(List<PurchaseInfo> purchases)
        {

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < purchases.Count; i++)
            {
                stringBuilder.AppendLine($"{i + 1}. " + purchases[i].ToString());
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Метод для удаления конкретной покупки из общего списка.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="number"></param>
        public static void DeletePurchases(long id, int number)
        {
            var purchases = ReadPurchase(id);

            if (purchases.Count < number || number <= 0)
            {
                throw new IndexOutOfRangeException();
            }

            using (JsonWriter fs = new JsonTextWriter(new StreamWriter($"{path}{id}.json")))
            {
                purchases.RemoveAt(number - 1);
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(fs, purchases);
            }
        }

        /// <summary>
        /// Метод для удаления диапазона покупок из общего списка. Перегрузка.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="leftBorder"></param>
        /// <param name="rightBorder"></param>
        public static void DeletePurchases(long id, int leftBorder, int rightBorder)
        {
            var purchases = ReadPurchase(id);

            if (purchases.Count < rightBorder || leftBorder <= 0 || rightBorder <= 0
                || rightBorder < leftBorder)
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = leftBorder - 1; i <= rightBorder - 1; rightBorder--)
            {
                purchases.RemoveAt(i);
            }
            using (JsonWriter fs = new JsonTextWriter(new StreamWriter($"{path}{id}.json")))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(fs, purchases);
            }
        }

        /// <summary>
        /// Метод, который меняет значение поля валюты 
        /// на привычный пользователю.
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string RenameCurrency(string currency)
        {
            if (currency == "RUB")
                return "РУБ";
            else if (currency == "USD")
                return "USD";
            else
                return "СУМ";
        }

        /// <summary>
        /// Обработка сообщений с покупками для извлечения важной информации
        /// и дальнейшего создания объекта класса PurchaseInfo.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PurchaseInfo PurchaseParsing(string message, long id)
        {
            string productName = String.Empty;
            double productCost = 0;
            string productCurrency = String.Empty;
            string productType = "Разное";
            DateTime productDate = DateTime.Now;

            string[] parsedInput = Regex.Replace(message, @"\s+", " ").Split(' ');

            // Price Parsing.
            int indexPrice = Array.FindIndex(parsedInput, e => double.TryParse(e, NumberStyles.Any,
              CultureInfo.InvariantCulture, out productCost));


            if (indexPrice < 1 || productCost < 0) return null;
            // Name Parsing.
            productName = string.Join(" ",
              parsedInput.TakeWhile(e => !double.TryParse(e, NumberStyles.Any,
              CultureInfo.InvariantCulture, out double price)));

            // If it's all good, create the object.

            // Date Parsing.
            int indexDate = Array.FindIndex(parsedInput, e => DateTime.TryParseExact(e, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out productDate));

            // Currency Parsing.
            int indexCurrency = indexPrice;
            if (indexPrice != parsedInput.Length - 1)
            {
                if (Regex.IsMatch(parsedInput[indexCurrency + 1], "^рубл.*$"))
                {
                    indexCurrency++;
                    productCurrency = "RUB";

                }
                else if (Regex.IsMatch(parsedInput[indexCurrency + 1], "^доллар.*$"))
                {
                    indexCurrency++;
                    productCurrency = "USD";

                }
                else if (Regex.IsMatch(parsedInput[indexPrice + 1], "^сум.*$"))
                {
                    indexCurrency++;
                    productCurrency = "UZS";

                }
                else
                {
                    User user = User.ReadJSON(id);
                    productCurrency = user.PreferableCurrency;
                }
            }
            else
                productCurrency = User.ReadJSON(id).PreferableCurrency;

            // Type Parsing.
            productType = String.Empty;

            if (indexCurrency != indexPrice && indexDate != -1
                 && indexCurrency != indexDate - 1)
            {
                for (int i = indexCurrency + 1; i < indexDate; i++)
                {
                    productType += parsedInput[i] + " ";
                }
            }
            else if (indexCurrency == indexPrice && indexDate != -1
                && indexDate != indexPrice + 1)
            {
                for (int i = indexPrice + 1; i < indexDate; i++)
                {
                    productType += parsedInput[i] + " ";
                }
            }
            else if (indexCurrency != indexPrice && indexDate == -1
                && indexCurrency != parsedInput.Length - 1 && indexCurrency != indexDate-1)
            {
                for (int i = indexCurrency + 1; i < parsedInput.Length; i++)
                {
                    productType += parsedInput[i] + " ";
                }
            }
            else if (indexCurrency == indexPrice
                && indexDate == -1 && indexPrice != parsedInput.Length - 1)
            {
                for (int i = indexPrice + 1; i < parsedInput.Length; i++)
                {
                    productType += parsedInput[i] + " ";
                }
            }
            else
            {
                productType = "Разное";
            }

            productType = productType.Trim();

            return new PurchaseInfo(productName, productCost, productCurrency,
              productType, productDate);
        }

        /// <summary>
        /// Переопределенный метод для вывода информации о покупке.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            
            return $"{Obj.Replace("_", "").Replace("`", "").Replace("*", "")} {Price} " +
                $"{RenameCurrency(Currency)} {Type.Replace("_", "").Replace("`", "").Replace("*", "")} " +
                $"{Date.ToShortDateString()}";
        }
    }
}
