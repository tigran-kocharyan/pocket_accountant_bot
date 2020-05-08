using System;
using System.Threading;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using BotLibrary;
using static BotLibrary.Phrases;
using static BotLibrary.Markups;
using static BotLibrary.Analysis;
//using System.Net;

//using System.Threading.Tasks;
//using MihaZupan;

namespace TelegramBot
{
    public class Program
    {
        /// <summary>
        /// Объект botClient интерфейса ITelegramBotClient необходим для использоания
        /// методов и событий.
        /// </summary>
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
            User.WriteJSON(new User(e.Message.Chat.Username, e.Message.Chat.Id));

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

                            case "Некорректные данные о покупке 🥴\n\n" + replyCheck:
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
            var chatID = e.CallbackQuery.Message.Chat.Id;
            try
            {
                switch (data)
                {
                    case null:
                        break;

                    case "0":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id);

                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: helpInput,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: helpMarkup);
                        break;

                    case "1":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id);

                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: helpWhy,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: helpMarkup);
                        break;

                    case "2":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id);

                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: helpFor,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: helpMarkup);
                        break;

                    case "3":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id);

                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: helpHow,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: helpMarkup);
                        break;

                    case "menu":
                        await botClient.AnswerCallbackQueryAsync
                           (e.CallbackQuery.Id);

                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: greetingMessage,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: menuMarkup);
                        break;

                    case "setting":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Выберите Валюту По-умолчанию 🗣");

                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: settingMessage + "Текущая валюта по-умолчанию: " +
                            User.CheckCurrency(User.ReadJSON(chatID).PreferableCurrency),
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: settingMarkup);
                        break;

                    case "usd":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Новая Валюта: USD 🗣");

                        User.EditJSON(chatID, "USD", User.ReadJSON(chatID));
                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: settingMessage + "Текущая валюта по-умолчанию: " +
                            User.CheckCurrency(User.ReadJSON(chatID).PreferableCurrency),
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: settingMarkup);
                        break;

                    case "rub":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Новая Валюта: РУБ 🗣");

                        User.EditJSON(chatID, "RUB", User.ReadJSON(chatID));
                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: settingMessage + "Текущая валюта по-умолчанию: " +
                            User.CheckCurrency(User.ReadJSON(chatID).PreferableCurrency),
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: settingMarkup);
                        break;

                    case "sum":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id, "Новая Валюта: СУМ 🗣");

                        User.EditJSON(chatID, "UZS", User.ReadJSON(chatID));
                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: settingMessage + "Текущая валюта по-умолчанию: " +
                            User.CheckCurrency(User.ReadJSON(chatID).PreferableCurrency),
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: settingMarkup);
                        break;

                    case "help":
                        await botClient.AnswerCallbackQueryAsync
                           (e.CallbackQuery.Id);

                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: helpMessage,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: helpMarkup);
                        break;

                    case "input":
                        try
                        {
                            await botClient.AnswerCallbackQueryAsync
                           (e.CallbackQuery.Id);

                            await botClient.SendTextMessageAsync(
                                chatId: chatID,
                                text: replyMessage,
                                replyMarkup: force,
                                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{new String('=', 30)}\nERROR: {ex.Message}");
                        }
                        break;

                    case "output":

                        long id = e.CallbackQuery.Message.Chat.Id;
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id);

                        if (File.Exists(@"../../../data/purchases/" + id + ".json"))
                        {
                            List<PurchaseInfo> purchases = PurchaseInfo.ReadPurchase(chatID);
                            StringBuilder stringBuilder = new StringBuilder();
                            for (int i = 0; i < purchases.Count; i++)
                            {
                                stringBuilder.AppendLine($"{i + 1}. " + purchases[i].ToString());
                            }
                            await botClient.SendTextMessageAsync(
                                chatId: chatID,
                                text: $"*Ваши покупки вида:*\n" +
                                $"_Название Цена Валюта Категория Дата_\n\n"
                                + stringBuilder.ToString(),
                                replyMarkup: analysisMarkup,
                                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown); ;
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(
                                chatId: chatID,
                                text: noJsonMessage,
                                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        }
                        break;

                    case "commands":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id);

                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: commandMessage,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: menuMarkup);
                        break;

                    case "graphic":
                        await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id);

                        var purchasesList = PurchaseInfo.ReadPurchase(chatID);
                        purchasesList = purchasesList.OrderBy(x => x.Date).ToList();
                        var purchasesSums = purchasesList.GroupBy(y => y.Date)
                            .Select(a => a.Sum(b => b.Price)).ToList();
                        var purchasesDates = purchasesList.Select(a => a.Date).
                            Distinct().ToList();

                        GraphicAnalysis(purchasesSums, purchasesDates, chatID);
                        botClient.SendPhotoAsync(chatId: chatID,
                            photo: new Telegram.Bot.Types.InputFiles.InputOnlineFile
                            ($"../../../data/{chatID}.png"),
                            caption: "Вот график Ваших расходов:");
                        break;

                    default:
                        Console.WriteLine("[Callback] Something Went Wrong!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{new String('=', 30)}\nERROR: " +
                    $"{ex.Message}\n{new String('=', 30)}");
            }
        }
    }
}