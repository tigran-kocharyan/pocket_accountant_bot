using System;
using System.IO;

namespace BotLibrary
{
    public class User
    {
        private static string path = @"../../../data/users/users.txt";
        private static string newLine = Environment.NewLine;

        public User(string username, long id)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                if (!isContains(id))
                    AddUser(username, id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"File Error with {username}. Error: {e}");
            }

        }
        private bool isContains(long id) => File.ReadAllText(path).Contains
            ($"ID: {id}");

        private void AddUser(string username, long id) => File.AppendAllText(path,
            $"[User] => Name: {username}, ID: {id}{newLine}");
    }
}

//($"[User] => Name: {username}, ID: {id}{newLine}");