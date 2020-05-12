using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using static BotLibrary.Phrases;
using static BotLibrary.Markups;

namespace BotLibrary
{
    public class CallbackHandler
    {
        public async static void HelpInput(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
                await botClient.AnswerCallbackQueryAsync
                                (e.CallbackQuery.Id);

                await botClient.EditMessageTextAsync(
                    chatId: chatID,
                    text: helpInput,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    messageId: e.CallbackQuery.Message.MessageId,
                    replyMarkup: helpMarkup);
            }
            catch
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }

        public async static void HelpWhy(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
                await botClient.AnswerCallbackQueryAsync
                                (e.CallbackQuery.Id);

                await botClient.EditMessageTextAsync(
                    chatId: chatID,
                    text: helpWhy,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    messageId: e.CallbackQuery.Message.MessageId,
                    replyMarkup: helpMarkup);
            }
            catch
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }

        public async static void HelpFor(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
                await botClient.AnswerCallbackQueryAsync
                                (e.CallbackQuery.Id);

                await botClient.EditMessageTextAsync(
                    chatId: chatID,
                    text: helpFor,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    messageId: e.CallbackQuery.Message.MessageId,
                    replyMarkup: helpMarkup);
            }
            catch
            {
                System.Console.WriteLine("[Markup] Error");
            }

        }

        public async static void HelpHow(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
                await botClient.AnswerCallbackQueryAsync
                                (e.CallbackQuery.Id);

                await botClient.EditMessageTextAsync(
                    chatId: chatID,
                    text: helpHow,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    messageId: e.CallbackQuery.Message.MessageId,
                    replyMarkup: helpMarkup);
            }
            catch
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }

        public async static void MenuShow(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
                await botClient.AnswerCallbackQueryAsync
                               (e.CallbackQuery.Id);

                await botClient.EditMessageTextAsync(
                    chatId: chatID,
                    text: greetingMessage,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    messageId: e.CallbackQuery.Message.MessageId,
                    replyMarkup: menuMarkup);
            }
            catch
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }

        public async static void SettingShow(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
                await botClient.AnswerCallbackQueryAsync
                                (e.CallbackQuery.Id, "Выберите Валюту По-умолчанию 🗣");

                await botClient.EditMessageTextAsync(
                    chatId: chatID,
                    text: settingMessage + "Текущая валюта по-умолчанию: " +
                    User.CheckCurrency(User.ReadJSON(chatID).PreferableCurrency),
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    messageId: e.CallbackQuery.Message.MessageId,
                    replyMarkup: settingMarkup);
            }
            catch
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }

        public async static void HelpShow(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
                await botClient.AnswerCallbackQueryAsync
                               (e.CallbackQuery.Id);

                await botClient.EditMessageTextAsync(
                    chatId: chatID,
                    text: helpMessage,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    messageId: e.CallbackQuery.Message.MessageId,
                    replyMarkup: helpMarkup);
            }
            catch
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }

        public async static void InputShow(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
                await botClient.AnswerCallbackQueryAsync
                               (e.CallbackQuery.Id);

                await botClient.SendTextMessageAsync(
                    chatId: chatID,
                    text: replyMessage,
                    replyMarkup: force,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
            }
            catch
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }

        public async static void OutputShow(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
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
                    await botClient.EditMessageTextAsync(
                        chatId: chatID,
                        text: $"*Ваши покупки вида:*\n" +
                        $"_Название Цена Валюта Категория Дата_\n\n"
                        + stringBuilder.ToString(),
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        messageId: e.CallbackQuery.Message.MessageId,
                        replyMarkup: analysisMarkup);

                }
                else
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatID,
                        text: noJsonMessage,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                }
            }
            catch
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }

        public async static void GoalShow(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;

                await botClient.AnswerCallbackQueryAsync
                            (e.CallbackQuery.Id);


                if (!Goal.HasGoal(chatID))
                {
                    await botClient.SendTextMessageAsync(
                    chatId: chatID,
                    text: noGoalReply,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    replyMarkup: force);
                }
                else
                {
                    Goal goal = Goal.ReadGoal(chatID);

                    await botClient.EditMessageTextAsync(
                    chatId: chatID,
                    text: $"*На данный момент Вы копите на {goal.GoalName}* 💸\n\n" +
                    $"До цели => *{goal.GoalPrice} {goal.GoalCurrency}*",
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    messageId: e.CallbackQuery.Message.MessageId,
                    replyMarkup: editGoalMarkup);
                }
            }
            catch
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }

        public async static void CommandsShow(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
                await botClient.AnswerCallbackQueryAsync
                                (e.CallbackQuery.Id);

                await botClient.EditMessageTextAsync(
                    chatId: chatID,
                    text: commandMessage,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    messageId: e.CallbackQuery.Message.MessageId,
                    replyMarkup: commandMarkup);
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }

        /// <summary>
        /// Метод для вывода графика расходов в чат.
        /// Использует System.Design для отрисовки Bitmap.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
        public async static void GraphicShow(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
                await botClient.AnswerCallbackQueryAsync
                                (e.CallbackQuery.Id);

                // Считывание всех покупок из JSON. Затем сортировка по дате и суммирование цен.
                var purchasesList = PurchaseInfo.ReadPurchase(chatID);
                purchasesList = purchasesList.OrderBy(x => x.Date).ToList();
                var purchasesSums = purchasesList.GroupBy(y => y.Date)
                    .Select(a => a.Sum(b => b.Price)).ToList();

                // Извлечение уникальных дат с помощью Distinct().
                var purchasesDates = purchasesList.Select(a => a.Date).
                    Distinct().ToList();

                // Вызов метода создания изображения графика и сохранение.
                Analysis.GraphicAnalysis(purchasesSums, purchasesDates, chatID);

                // С помощью потока загружаем изображение в чат.
                using (var stream = File.Open($"../../../data/graphics/{chatID}.png", FileMode.Open))
                {
                    var file = new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream);
                    file.FileName = "Graphic";
                    await botClient.SendPhotoAsync(chatId: chatID,
                    photo: file,
                    caption: "Вот график Ваших расходов:");
                }
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }

        /// <summary>
        /// Изменяет текушую валюту в файле preferances на USD.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
        public async static void ChangeUSD(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
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
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("[Markup] Error");
            } 
        }

        /// <summary>
        /// Изменяет текушую валюту в файле preferances на RUB.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
        public async static void ChangeRUB(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
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
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }

        /// <summary>
        /// Изменяет текушую валюту в файле preferances на UZS.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
        public async static void ChangeUZS(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
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
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }
    }
}
