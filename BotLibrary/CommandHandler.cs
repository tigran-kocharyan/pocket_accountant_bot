using System;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using static BotLibrary.Phrases;
using static BotLibrary.Markups;

namespace BotLibrary
{
    public class CommandHandler
    {
        public async static void DoStart(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                await botClient.SendTextMessageAsync(e.Message.Chat,
                text: greetingMessage,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                replyMarkup: menuMarkup);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{new String('=', 30)}\nERROR: {ex.Message}");
            }
        }

        public async static void AddMoneyToGoal(MessageEventArgs e, ITelegramBotClient botClient)
        {
            if (double.TryParse(e.Message.Text, NumberStyles.Any,
                  CultureInfo.InvariantCulture, out double money) && money>=1)
            {
                Goal goal = Goal.ReadGoal(e.Message.Chat.Id);
                goal.GoalPrice -= money;
                Goal.WriteGoal(e.Message.Chat.Id, goal);
                string answer = Goal.CheckGoal(e.Message.Chat.Id, goal);
                if (answer.Contains("Поздравляю"))
                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: answer,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: menuMarkup);
                else
                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: answer,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: editGoalMarkup);
            }
            else
            {
                await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "*Некорректные данные о сумме денег 🥴*\n\n" + addMoneyReply,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: force);
            }
            // проверить текст на то, что там только сумма денег.
            // считать текущую гоал 
            // отнять от нее сумму и проверить на CheckGoal
            // вывести сообщение из CheckGoal и обновить маркап.
        }

        public async static void AddGoal(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                Goal goal = Goal.GoalParsing(e.Message.Text, e.Message.Chat.Id);
                if (goal != null)
                {
                    Goal.WriteGoal(e.Message.Chat.Id, goal);
                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "*Цель Успешно Добавлена ✅*\n\nВыберите дальнейшие действия.",
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: menuMarkup
                        );
                }
                else
                {
                    throw new ArgumentException("Invalid Data!");
                }
            }
            catch (Exception)
            {
                await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "*Некорректные данные о цели 🥴*\n\n" + noGoalCheck,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: force);
            }
        }

        public async static void GetExpense(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                if (File.Exists(@"../../../data/purchases/" + e.Message.Chat.Id + ".json"))
                {
                    List<PurchaseInfo> purchases = PurchaseInfo.ReadPurchase(e.Message.Chat.Id);

                    string purchasesString = PurchaseInfo.PurchasesToString(purchases);

                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: $"*Ваши покупки вида:*\n_Название Цена Валюта Категория Дата_\n\n"
                        + purchasesString + "\n*Так же Вы можете получить анализ, " +
                        "нажав на соответствующую кнопку ниже 📊*",
                        replyMarkup: analysisMarkup,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                }
                else
                {
                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: noJsonMessage,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{new String('=', 30)}\nERROR: {ex.Message}");
            }
        }

        public async static void FillExpense(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                // Переменная для подсчета количества успешных покупок.
                int temp = 0;

                string[] textLines = Regex.Replace(e.Message.Text, @"\n+", "\n").Split('\n');
                for (int i = 0; i < textLines.Length; i++)
                {
                    PurchaseInfo purchase = PurchaseInfo.PurchaseParsing
                        (textLines[i], e.Message.Chat.Id);
                    if (purchase != null)
                    {
                        PurchaseInfo.WritePurchase(e.Message.Chat.Id, purchase);
                        temp++;
                    }
                    else
                    {
                        Console.WriteLine($"[Purchase] Wrong Info from {e.Message.Chat.Id}");
                    }
                }
                if (temp == 0)
                    await botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "*Некорректные данные о покупке или покупках 🥴*\n\n" + replyMessage,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            replyMarkup: force);
                else
                    await botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: $"*Успешно добавлено {temp} из {textLines.Length} покупок ✅*\n\n"
                            + replyMessage,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            replyMarkup: force
                            );
            }
            catch (Exception)
            {
                Console.WriteLine($"[Purchase] Wrong Info from {e.Message.Chat.Id}");
            }
        }

        public async static void AddExpense(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: replyMessage,
                    replyMarkup: force,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{new String('=', 30)}\nERROR: {ex.Message}");
            }
        }

        public async static void DeleteExpense(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                long id = e.Message.Chat.Id;
                if (int.TryParse(e.Message.Text, out int number))
                {
                    PurchaseInfo.DeletePurchases(id, number);
                }
                else
                {
                    string[] borders = e.Message.Text.Split('-');
                    if (borders.Length == 2 && int.TryParse(borders[0], out int leftBorder)
                        && int.TryParse(borders[1], out int rightBorder))
                    {
                        PurchaseInfo.DeletePurchases(id, leftBorder, rightBorder);
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }

                if (PurchaseInfo.ReadPurchase(id).Count == 0)
                {
                    File.Delete(@"../../../data/purchases/" + id + ".json");
                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "*Ваш список покупок был обновлен 📊*\n\n"
                        + "*На данный момент Ваш список покупок пуст ☹️*",
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: menuMarkup);
                }
                else
                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "*Ваш список покупок был обновлен 📊*\n\n"
                        + PurchaseInfo.PurchasesToString(PurchaseInfo.ReadPurchase(id)),
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: analysisMarkup);
            }
            catch (Exception)
            {
                await botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "*Некорректные данные о границах 🥴*\n\n" + deletePurchasesReply,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            replyMarkup: force);
            }
        }

        public async static void ShowCommands(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                await botClient.SendTextMessageAsync(e.Message.Chat, commandMessage,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                replyMarkup: commandMarkup);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{new String('=', 30)}\nERROR: {ex.Message}");
            }
        }

        public async static void ShowHelp(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                await botClient.SendTextMessageAsync(e.Message.Chat,
                text: helpMessage,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                replyMarkup: helpMarkup);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{new String('=', 30)}\nERROR: {ex.Message}");
            }

        }

        /// <summary>
        /// Вывод сообщения об ошибке в чат с пользователем.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
        public async static void ShowError(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                replyMarkup: menuMarkup,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                text: errorMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{new String('=', 30)}\nERROR: {ex.Message}");
            }
        }
    }
}
