using System;
using System.IO;
using Newtonsoft.Json;

namespace BotLibrary
{
    public class User
    {
        private string username;
        public string Username
        {
            get{
                return username;
            }
            set
            {
                if (value == String.Empty)
                {
                    username = "NoName";
                }
                else
                {
                    username = "@"+value;
                }
            }
        }

        public long Id { get; set; }
        public string PreferableLanguage { get; set; }
        public string PreferableCurrency { get; set; }

        public static string pathUsers = @"../../../data/users/users.txt";
        public static string pathPreferences = @"../../../data/preferences/";

        public User(string username, long id)
        {
            try
            {
                if (!File.Exists(pathUsers))
                    File.Create(pathUsers);
                if (!isContains(id))
                    AddUser(username, id);

                Username = username;
                Id = id;
                PreferableCurrency = "RUB";
                PreferableLanguage = "Russian";
            }
            catch (Exception e)
            {
                Console.WriteLine($"File Error with {username}. Error: {e}");
            }

        }
        private bool isContains(long id) => File.ReadAllText(pathUsers).Contains
            ($"ID: {id}");
        private void AddUser(string username, long id) => File.AppendAllText(pathUsers,
            $"[User] => ID: {id}\n | {username}");

        public static void AddJSON(User user)
        {
            using (JsonWriter fs = new JsonTextWriter
                (new StreamWriter($"{pathPreferences}{user.Id}.json")))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(fs, user);
            }
        }

        public static User ReadJSON(long id)
        {
            using (JsonReader fs = new JsonTextReader(new StreamReader($"{pathPreferences}{id}.json")))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                User user = jsonSerializer.Deserialize(fs, typeof(User)) as User;
                return user;
            }
        }

        public static string CheckCurrency(string currency)
        {
            if (currency == "RUB")
                return "Рубль 🇷🇺";
            else if (currency == "USD")
                return "Доллар 🇺🇸";
            else
                return "Сум 🇺🇿";
        }
    }
}

//($"[User] => Name: {username}, ID: {id}{newLine}");