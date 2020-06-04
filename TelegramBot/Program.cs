using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using BotLibrary;
using static BotLibrary.Phrases;
using static BotLibrary.Markups;
using System.IO;

namespace TelegramBot
{
    public class Program
    {
        /// <summary>
        /// Объект botClient интерфейса ITelegramBotClient необходим для использоания
        /// методов и событий.
        /// </summary>
        private static ITelegramBotClient botClient;

        public static void Main(string[] args)
        {
            try
            {
                // Создание объекта класса TelegramBotClient, который представляет нашего бота.
                // В параметрах создания бота использует его уникальный ТОКЕН.
                botClient = new TelegramBotClient("PUT-YOUR-TOKEN-IN-HERE")
                { Timeout = TimeSpan.FromSeconds(10) };

                // Выводим в консоль время начала работы бота.
                Console.WriteLine($"[{DateTime.Now}]: Bot is running...");

                // Подписываем методы-обработчики на событие получения сообщения
                // и на событие получение Callback Query.
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

        /// <summary>
        /// Метод, который обрабатывает текст сообщения и в завимисоти от него вызывает методы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            // Записываем информацию с натсройками нового пользователя, если такого
            // еще не существует.
            if (!File.Exists(@"../../../data/preferences/"+e.Message.Chat.Id+".json"))
            {
                User.WriteJSON(new User(e.Message.Chat.Username, e.Message.Chat.Id));
            }

            var text = e?.Message?.Text;
            Console.WriteLine($"[User] @{e.Message.Chat.Username} with [ID] {e.Message.Chat.Id} is texting...");

            // Вызываем необходимые методы.
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

        /// <summary>
        /// Метод, который обрабатывает запросы обратного вызова и в завимисоти от него вызывает методы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static async void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var data = e.CallbackQuery.Data;
            var chatID = e.CallbackQuery.Message.Chat.Id;

            // Вызываем необходимые методы.
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
                        CallbackHandler.OutputTodayShow(e, botClient);
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

                    case "filterPurchase":
                        CallbackHandler.FilterShow(e, botClient);
                        break;

                    case "today":
                        CallbackHandler.OutputTodayShow(e, botClient);
                        break;

                    case "month":
                        CallbackHandler.OutputMonthShow(e, botClient);
                        break;

                    case "alltime":
                        CallbackHandler.OutputShow(e, botClient);
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
