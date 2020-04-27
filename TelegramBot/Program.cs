using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using BotLibrary;
//using System.Net;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using MihaZupan;

namespace TelegramBot
{
    public class Program
    {
        private static ITelegramBotClient botClient;
        public static string replyMessage = "Введите Вашу покупку в виде " +
            "{Предмет} {Стоимость в цифрах} {Валюта}" + Environment.NewLine +
            $"Пример: _Резиновый шланг 100 рублей_" + Environment.NewLine +
            "*ВАЖНО, ЧТОБЫ СТОИМОСТЬ И ВАЛЮТА ШЛИ В КОНЦЕ!*";

        public static ReplyKeyboardMarkup markup = new ReplyKeyboardMarkup();

        static void Main(string[] args)
        {
            try
            {
                //var proxy = new HttpToSocks5Proxy("217.196.81.221", 43870);
                botClient = new TelegramBotClient("788209639:AAEcBsecEd_CCzu2uOrYo80WdzSyN7lSsC0") { Timeout = TimeSpan.FromSeconds(10) };

                markup.Keyboard =
            new KeyboardButton[][]
            {
                new KeyboardButton[]
                {
                new KeyboardButton("Дубки"),
                },

                new KeyboardButton[]
                {
                new KeyboardButton("Одинцово")
                }
            };

                Console.WriteLine($"[{DateTime.Now}]: Bot is running...");
                Console.WriteLine("Current $USD rate is " + CurrencyParser.getCurrency());

                botClient.OnMessage += Bot_OnMessage;

                botClient.StartReceiving();
                Thread.Sleep(int.MaxValue);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{new String('=', 30)}\nERROR: {e.Message}");
            }
        }

        private static void Bot_OnMessage(object sender, MessageEventArgs e) // Не забыть про async.
        {
            new User("@" + e.Message.Chat.Username, e.Message.Chat.Id);

            var text = e?.Message?.Text;
            Console.WriteLine($"[User] @{e.Message.Chat.Username} with [ID] {e.Message.Chat.Id} is texting...");

            try
            {
                switch (text)
                {
                    case null:
                        break;

                    case "/start":
                        CommandHandler.DoStart(e, botClient);
                        break;

                    case "/commands":
                        CommandHandler.ShowCommands(e, botClient);
                        break;

                    case "/help":
                        CommandHandler.ShowHelp(e, botClient);
                        break;

                    case "/add_expense":
                        CommandHandler.AddExpense(e, botClient);
                        break;

                    default:
                        switch (e?.Message?.ReplyToMessage?.Text)
                        {
                            case "Введите Вашу покупку в виде:\n" +
            "{Предмет} {Цена} {Валюта}\n\n/help для помощи...":
                                CommandHandler.FillExpense(e, botClient);
                                break;

                            default:
                                CommandHandler.ShowError(e, botClient);
                                break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{new String('=', 30)}\nERROR: {ex.Message}");
            }
        }

        //if (text == null)
        //{
        //    return;
        //}
        //else if (text == "/start")
        //{
        //    CommandHandler.DoStart(e);
        //}
        //else if (text == "/commands")
        //{
        //    CommandHandler.ShowCommands(e);
        //}
        ////else if (text == "/getStick")
        ////{
        ////    SendSticker(e);
        ////}
        ////else if (text == "/getCurrency")
        ////{
        ////    SendCurrency(e);
        ////}

        //else if (text == "/add_expense")
        //{
        //    CommandHandler.AddExpense(e);
        //}
        //else if (e?.Message?.ReplyToMessage?.Text == replyMessage)
        //{
        //    CommandHandler.FillExpense(e);
        //}

        //else
        //{
        //    await botClient.SendTextMessageAsync(e.Message.Chat, "Что-то пошло не так 😞. Используйте комманды для общения с ботом.");
        //}
    }
}