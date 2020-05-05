using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Args;
using static BotLibrary.Phrases;
using static BotLibrary.Markups;
using System.Collections.Generic;
using System.Text;

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

        public async static void ShowCommands(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                await botClient.SendTextMessageAsync(e.Message.Chat, commandMessage,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                replyMarkup: menuMarkup);
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

        public async static void GetExpense(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                if (File.Exists(@"../../../data/purchases/" + e.Message.Chat.Id + ".json"))
                {
                    List<PurchaseInfo> purchases = PurchaseInfo.ReadPurchase(e.Message.Chat.Id);
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < purchases.Count; i++)
                    {
                        stringBuilder.AppendLine($"{i+1}. "+purchases[i].ToString());
                    }
                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: $"*Ваши покупки вида:*\n_Название Цена Валюта Категория Дата_\n\n" 
                        + stringBuilder.ToString(),
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
                PurchaseInfo purchase = PurchaseInfo.PurchaseParsing
                    (e.Message.Text, e.Message.Chat.Id);
                //string[] text = Regex.Replace(e.Message.Text, @"\s+", " ").Split(' ');

                if (purchase != null)
                {
                    PurchaseInfo.WritePurchase(e.Message.Chat.Id, purchase);

                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "*Успешно добавлено ✅*\n\n" + replyMessage,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: force
                        );
                }
                //    ExpensesTable.WriteCSV(
                //        id: e.Message.Chat.Id.ToString(),
                //        obj: string.Join(" ", text.Take(text.Length - 2)),
                //        price: double.Parse(text[text.Length - 2]),
                //        currency: text[text.Length - 1]);
                else
                {
                    throw new ArgumentException("Invalid Data!");
                }

            }
            catch (Exception)
            {
                await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "*Некорректные данные о покупке 🥴*\n\n" + replyMessage,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: force);
            }
        }

        public async static void ShowError(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
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
