using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using BotLibrary;
using System.IO;
using static BotLibrary.Phrases;
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
                //var proxy = new HttpToSocks5Proxy("p.webshare.io", 1080, "kgeylycq-1", "cnhaxv69p8lf");
                //proxy.ResolveHostnamesLocally = false;
                botClient = new TelegramBotClient("788209639:AAEcBsecEd_CCzu2uOrYo80WdzSyN7lSsC0")
                { Timeout = TimeSpan.FromSeconds(10) };

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
            User.AddJSON(new User(e.Message.Chat.Username, e.Message.Chat.Id));

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
                            case replyCheck:
                                CommandHandler.FillExpense(e, botClient);
                                break;

                            case "Успешно добавлено ✅\n\n" + replyCheck:
                                CommandHandler.FillExpense(e, botClient);
                                break;

                            case "Некорректные данные о покупке 🥴*\n\n" + replyCheck:
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

                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Помогаю 🗣");

                        await botClient.EditMessageTextAsync(
                            chatId: e.CallbackQuery.From.Id,
                            text: helpInput,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: CommandHandler.helpMarkup
                            );
                        break;

                    case "1":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Помогаю 🗣");

                        await botClient.EditMessageTextAsync(
                            chatId: e.CallbackQuery.From.Id,
                            text: helpWhy,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: CommandHandler.helpMarkup
                            );
                        break;

                    case "2":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Помогаю 🗣");

                        await botClient.EditMessageTextAsync(
                            chatId: e.CallbackQuery.From.Id,
                            text: helpFor,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: CommandHandler.helpMarkup
                            );
                        break;

                    case "3":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Помогаю 🗣");

                        await botClient.EditMessageTextAsync(
                            chatId: e.CallbackQuery.From.Id,
                            text: helpHow,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: CommandHandler.helpMarkup
                            );
                        break;

                    case "setting":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Помогаю 🗣");

                        await botClient.EditMessageTextAsync(
                            chatId: e.CallbackQuery.From.Id,
                            text: settingMessage + "Текущая валюта по умолчанию: " + 
                            User.CheckCurrency(User.ReadJSON(e.CallbackQuery.From.Id).PreferableCurrency),
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: CommandHandler.settingMarkup
                            );
                        break;

                    case "menu":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Помогаю 🗣");

                        await botClient.EditMessageTextAsync(
                            chatId: e.CallbackQuery.From.Id,
                            text: greetingMessage,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: CommandHandler.menuMarkup
                            );
                        break;

                    //case "usd":
                    //    await botClient.AnswerCallbackQueryAsync
                    //        (e.CallbackQuery.Id, "Помогаю 🗣");

                    //    await botClient.EditMessageTextAsync(
                    //        chatId: e.CallbackQuery.From.Id,
                    //        text: greetingMessage,
                    //        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    //        messageId: e.CallbackQuery.Message.MessageId,
                    //        replyMarkup: CommandHandler.menuMarkup
                    //        );
                    //    break;

                    //case "rub":
                    //    await botClient.AnswerCallbackQueryAsync
                    //        (e.CallbackQuery.Id, "Помогаю 🗣");

                    //    await botClient.EditMessageTextAsync(
                    //        chatId: e.CallbackQuery.From.Id,
                    //        text: greetingMessage,
                    //        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    //        messageId: e.CallbackQuery.Message.MessageId,
                    //        replyMarkup: CommandHandler.menuMarkup
                    //        );
                    //    break;

                    //case "sum":
                    //    await botClient.AnswerCallbackQueryAsync
                    //        (e.CallbackQuery.Id, "Помогаю 🗣");

                    //    await botClient.EditMessageTextAsync(
                    //        chatId: e.CallbackQuery.From.Id,
                    //        text: greetingMessage,
                    //        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    //        messageId: e.CallbackQuery.Message.MessageId,
                    //        replyMarkup: CommandHandler.menuMarkup
                    //        );
                    //    break;

                    case "help":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Помогаю 🗣");

                        await botClient.EditMessageTextAsync(
                            chatId: e.CallbackQuery.From.Id,
                            text: helpMessage,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: CommandHandler.helpMarkup
                            );
                        break;

                    //case "input":
                    //    await botClient.AnswerCallbackQueryAsync
                    //        (e.CallbackQuery.Id, "Помогаю 🗣");

                    //    break;

                    case "output":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Помогаю 🗣");

                        if (File.Exists(@"../../../data/tables/" + 
                            e.CallbackQuery.Message.Chat.Id + ".csv"))
                        {
                            string expenses = ExpensesTable.ReadCSV
                                (e.CallbackQuery.Message.Chat.Id.ToString());
                            await botClient.SendTextMessageAsync(
                                chatId: e.CallbackQuery.From.Id,
                                text: expenses);
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(
                                chatId: e.CallbackQuery.From.Id,
                                text: noJsonMessage,
                                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        }
                        break;
                    case "commands":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Помогаю 🗣");

                        await botClient.EditMessageTextAsync(
                            chatId: e.CallbackQuery.From.Id,
                            text: commandMessage,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: CommandHandler.menuMarkup
                            );
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