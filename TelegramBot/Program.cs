﻿using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
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

        static void Main(string[] args)
        {
            try
            {
                //var proxy = new HttpToSocks5Proxy("217.196.81.221", 43870);
                botClient = new TelegramBotClient("788209639:AAEcBsecEd_CCzu2uOrYo80WdzSyN7lSsC0") { Timeout = TimeSpan.FromSeconds(10) };

                Console.WriteLine($"[{DateTime.Now}]: Bot is running...");
                Console.WriteLine("Current $USD rate is " + CurrencyParser.getCurrency());

                botClient.OnMessage += Bot_OnMessage;
                botClient.OnCallbackQuery += Bot_OnCallbackQuery;

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

                    case "/get_expense":
                        CommandHandler.GetExpense(e, botClient);
                        break;

                    default:
                        switch (e?.Message?.ReplyToMessage?.Text)
                        {
                            case CommandHandler.replyCheck:
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

        private static async void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var data = e.CallbackQuery.Data;

            try
            {
                switch (data)
                {
                    case null:
                        break;

                    case "0":
                        //string outputMessage0 = "Чтобы правильно ввести данные, " +
                        //    "Вам необходимо соблюдать правила ✅\n" +
                        //    "1. Вызов `/get_expenses` необходим для начала ввода.\n\n" +
                        //    "2. Затем введите в ответном сообщении свою покупку в формате:\n" +
                        //    "       `{Продукт} {Цена} {Валюта} {Тип}`\n" +
                        //    "       По умолчанию:\n" +
                        //    "       Валюта - _Рубль_ и Тип - _Разное_.\n\n" +
                        //    "3. Пример ввода данных:\n" +
                        //    "       _Книга Шилдта 1000 рублей_";

                        string outputMessage0 = "Чтобы правильно ввести данные, " +
                            "Вам необходимо соблюдать правила ✅\n\n" +
                            "1. Вызов `/get_expenses` необходим для начала ввода.\n\n" +
                            "2. Затем введите в ответном сообщении свою покупку в формате:\n" +
                            "    `{Продукт} {Цена} {Валюта}`\n" +
                            "       По умолчанию: Валюта - _Рубль_.\n\n" +
                            "3. Пример ввода данных:\n" +
                            "       _Книга Шилдта 1000 рублей_";

                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Ввод данных 👩‍💻");

                        await botClient.SendTextMessageAsync(
                            chatId: e.CallbackQuery.From.Id,
                            text: outputMessage0,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        break;

                    case "1":
                        string username = "@Tigran_K";
                        string outputMessage1 = "Большинство ошибок возникает из-за 👨🏾‍💻\n" +
                            "1. Неправильный ввод команд.\n" +
                            "       _В таком случае, `/commands` спасёт Вас.\n\n" +
                            "2. Неправильный ввод данных для заполнения покупок.\n" +
                            "       _Во избежание этих ошибок, воспользуйтесь кнопкой\"Ввод данных\" в помощнике,_ " +
                            "_где уточняются все аспекты ввода._\n\n" +
                            $"3. Так же, Вы можете написать сообщение автору `{username}`, чтобы решить проблему 🦸🏻‍♂️";
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Почему ошибка? 😡");

                        await botClient.SendTextMessageAsync(
                            chatId: e.CallbackQuery.From.Id,
                            text: outputMessage1,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        break;

                    case "2":
                        string outputMessage2 = "       В первую очередь, Бот поможет Вам не \"перетратить\" деньги," +
                            "сообщив, что установленный Вам лимит стремится к нулю 😦\n\n" +
                            "       Так же Карманный Бухгалтер поможет Вам всегда " +
                            "быть в курсе всех Ваших покупок" +
                            ",предоставив список за день/неделю/месяц 💁‍♂️\n\n" +
                            "       Исходя из опроса, многим людям достаточно " +
                            "сложно держать в голове все свои расходы." +
                            "Поэтому, Карманный Бухгалтер предлагает переложить эту обязанность на него 🤖";

                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Зачем мне Бот? 🤡");

                        await botClient.SendTextMessageAsync(
                            chatId: e.CallbackQuery.From.Id,
                            text: outputMessage2,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        break;

                    case "3":
                        string outputMessage3 = "       Карманный Бухгалтер 🤖 запоминает все введенные Ваши покупки и " +
                            "_*сохраняет, анализирует и выводит*_ Вам их, спасая Вас от огромных списков 🙌";
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Как работает Бот 🤔");

                        await botClient.SendTextMessageAsync(
                            chatId: e.CallbackQuery.From.Id,
                            text: outputMessage3,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        break;

                    default:
                        Console.WriteLine("[Callback] Something Went Wrong!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{new String('=', 30)}\nERROR: {ex.Message}");
            }
        }
    }
}