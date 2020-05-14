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
                        if (e?.Message?.ReplyToMessage?.Text != null)
                        {
                            if(e.Message.ReplyToMessage.Text.Contains(replyCheck))
                                CommandHandler.FillExpense(e, botClient);
                            else if(e.Message.ReplyToMessage.Text.Contains(deletePurchasesCheck))
                                CommandHandler.DeleteExpense(e, botClient);
                            else if(e.Message.ReplyToMessage.Text.Contains(addMoneyReply))
                                CommandHandler.AddMoneyToGoal(e, botClient);
                            else if(e.Message.ReplyToMessage.Text.Contains(noGoalCheck))
                                CommandHandler.AddGoal(e, botClient);
                        }
                        else
                            CommandHandler.ShowError(e, botClient);
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
                        CallbackHandler.HelpInput(e, botClient);
                        break;

                    case "1":
                        CallbackHandler.HelpWhy(e, botClient);
                        break;

                    case "2":
                        CallbackHandler.HelpFor(e, botClient);
                        break;

                    case "3":
                        CallbackHandler.HelpHow(e, botClient);
                        break;

                    case "menu":
                        CallbackHandler.MenuShow(e, botClient);
                        break;

                    case "setting":
                        CallbackHandler.SettingShow(e, botClient);
                        break;

                    case "usd":
                        CallbackHandler.ChangeUSD(e, botClient);
                        break;

                    case "rub":
                        CallbackHandler.ChangeRUB(e, botClient);
                        break;

                    case "sum":
                        CallbackHandler.ChangeUZS(e, botClient);
                        break;

                    case "help":
                        CallbackHandler.HelpShow(e, botClient);
                        break;

                    case "input":
                        try
                        {
                            CallbackHandler.InputShow(e, botClient);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{new String('=', 30)}\nERROR: {ex.Message}");
                        }
                        break;

                    case "output":
                        CallbackHandler.OutputShow(e, botClient);
                        break;

                    case "commands":
                        CallbackHandler.CommandsShow(e, botClient);
                        break;

                    case "goal":
                        CallbackHandler.GoalShow(e, botClient);
                        break;

                    case "pie":
                        CallbackHandler.PieShow(e, botClient);
                        break;

                    case "graphic":
                        CallbackHandler.GraphicShow(e, botClient);
                        break;

                    case "deletePurchase":
                        await botClient.AnswerCallbackQueryAsync
                                        (e.CallbackQuery.Id);
                        Goal.DeleteGoal(chatID);
                        await botClient.SendTextMessageAsync(
                            chatId: chatID,
                            text: deletePurchasesReply,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            replyMarkup: force);
                        break;

                    case "deleteGoal":
                        await botClient.AnswerCallbackQueryAsync
                                        (e.CallbackQuery.Id);
                        Goal.DeleteGoal(chatID);
                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: "Цель успешно удалена ✅",
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: menuMarkup);
                        break;

                    case "addGoal":
                        await botClient.AnswerCallbackQueryAsync
                                        (e.CallbackQuery.Id);

                        await botClient.SendTextMessageAsync(
                            chatId: chatID,
                            text: addMoneyReply,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            replyMarkup: force);
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