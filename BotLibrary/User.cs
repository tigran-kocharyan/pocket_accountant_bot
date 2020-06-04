using System;
using System.IO;
using Newtonsoft.Json;

namespace BotLibrary
{
    /// <summary>
    /// Класс, который содержит методы для работы с пользователями
    /// и информацию о пользователях.
    /// </summary>
    public class User
    {
        // Поля, которые хранят информацию о пользователе.
        private string username;
        public string Username
        {
            get{
                return username;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    username = "NoName";
                }
                else
                {
                    username = value;
                }
            }
        }

        public long Id { get; set; }
        public string PreferableLanguage { get; set; }
        public string PreferableCurrency { get; set; }

        public static string pathUsers = @"../../../data/users/users.txt";
        public static string pathPreferences = @"../../../data/preferences/";

        /// <summary>
        /// Конструктор для создания объектов класса User.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="id"></param>
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

        /// <summary>
        /// Метод проверяет, существует ли в файле с пользователь id
        /// пользователя.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool isContains(long id) => File.ReadAllText(pathUsers).Contains
            ($"ID: {id}");
        
        /// <summary>
        /// Добавляет ID и Username пользователя в файл
        /// с информацией о пользователях.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="id"></param>
        private void AddUser(string username, long id) => File.AppendAllText(pathUsers,
            $"[User] => ID: {id} | {username}\n");

        /// <summary>
        /// Создание JSON файла с настройками пользователя.
        /// </summary>
        /// <param name="user"></param>
        public static void WriteJSON(User user)
        {
            using (JsonWriter fs = new JsonTextWriter
                (new StreamWriter($"{pathPreferences}{user.Id}.json")))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(fs, user);
            }
        }

        /// <summary>
        /// Чтение файла настроек пользователя.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static User ReadJSON(long id)
        {
            using (JsonReader fs = new JsonTextReader(new StreamReader($"{pathPreferences}{id}.json")))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                User user = jsonSerializer.Deserialize(fs, typeof(User)) as User;
                return user;
            }
        }

        /// <summary>
        /// Редактирование файла с настройками пользователя.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currency"></param>
        /// <param name="user"></param>
        public static void EditJSON(long id, string currency, User user)
        {
            using (JsonWriter fs = new JsonTextWriter(new StreamWriter($"{pathPreferences}{id}.json")))
            {
                user.PreferableCurrency = currency;
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(fs, user);
            }
        }

        /// <summary>
        /// Метод, который возвращает более привычную версию валюты
        /// пользователю.
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
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