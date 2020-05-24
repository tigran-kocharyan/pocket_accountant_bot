using System;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;

namespace BotLibrary
{
    /// <summary>
    /// Класс для создания цели покупки пользователя.
    /// </summary>
    public class Goal
    {
        public static string goalPath = "../../../data/goals/";

        public string GoalName { get; set; }
        public double GoalPrice { get; set; }
        public string GoalCurrency { get; set; }

        public Goal(string name, double price,string currency)
        {
            GoalName = name;
            GoalPrice = price;
            GoalCurrency = PurchaseInfo.RenameCurrency(currency);
        }

        public static void WriteGoal(long id, Goal goal)
        {
            using (JsonWriter fs = new JsonTextWriter(new StreamWriter($"{goalPath}{id}.json")))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(fs, goal);
            }
        }

        public static Goal ReadGoal(long id)
        {
            using (JsonReader fs = new JsonTextReader(new StreamReader($"{goalPath}{id}.json")))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                Goal goal = jsonSerializer.Deserialize(fs, typeof(Goal)) as Goal;
                return goal;
            }
        }

        public static string CheckGoal(long id, Goal goal)
        {
            if (goal.GoalPrice <= 0)
            {
                DeleteGoal(id);
                return "*Поздравляю 🎉*\n\n" +
                    $"Вы накопили свои сбережения и теперь можете приобрести *{goal.GoalName}*";
            }
            else
            {
                WriteGoal(id, goal);
                return $"*Сбережения обнолены 📊*\n\nНа данный момент Вам осталось " +
                    $"*{goal.GoalPrice} {goal.GoalCurrency}*\n\n" +
                    "*Не останавливайтесь, Вы почти у цели!*";
            }
        }

        public static void DeleteGoal(long id)
        {
            try
            {
                File.Delete($"{goalPath}{id}.json");
            }
            catch (Exception)
            {
                Console.WriteLine("[Delete] Problems while deleting.");
            }
        }

        public static Goal GoalParsing(string message, long id)
        {
            try
            {
                string productName = String.Empty;
                double productCost = 0;
                string productCurrency = User.ReadJSON(id).PreferableCurrency;

                string[] parsedInput = Regex.Replace(message, @"\s+", " ").Split(' ');

                // Price Parsing.
                int indexPrice = Array.FindIndex(parsedInput, e => double.TryParse(e, NumberStyles.Any,
                  CultureInfo.InvariantCulture, out productCost));

                if (indexPrice < 1 || productCost <= 0) return null;

                // Name Parsing.
                productName = string.Join(" ",
                  parsedInput.TakeWhile(e => !double.TryParse(e, NumberStyles.Any,
                  CultureInfo.InvariantCulture, out double price)));

                return new Goal(productName, productCost, productCurrency);
            }
            catch (Exception)
            {
                Console.WriteLine("[Parsing] Wrong parsing data.");
                return null;
            }
            
        }

        public static bool HasGoal(long id)
        {
            return File.Exists($"../../../data/goals/{id}.json");
        }
    }
}
