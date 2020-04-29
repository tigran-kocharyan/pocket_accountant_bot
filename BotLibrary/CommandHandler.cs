using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using static BotLibrary.Phrases;

namespace BotLibrary
{
    public class CommandHandler
    {
        public static Telegram.Bot.Types.ReplyMarkups.ForceReplyMarkup force =
            new Telegram.Bot.Types.ReplyMarkups.ForceReplyMarkup();

        public static InlineKeyboardMarkup helpMarkup =
            new InlineKeyboardMarkup(new InlineKeyboardButton[][]
{
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Ввод данных 👩‍💻","0"),
                    InlineKeyboardButton.WithCallbackData("Почему ошибка? 😡","1")
                },

                new []
                {
                    InlineKeyboardButton.WithCallbackData("Зачем мне Бот? 🤡","2"),
                    InlineKeyboardButton.WithCallbackData("Как работает Бот 🤔","3")
                },

                new []
                {
                    InlineKeyboardButton.WithUrl("Написать Разработчику 🥰","https://vk.com/k_tigran")
                },

                new []
                {
                    InlineKeyboardButton.WithCallbackData("Главное меню 🔠","menu")
                }
});

        public static InlineKeyboardMarkup settingMarkup =
            new InlineKeyboardMarkup(new InlineKeyboardButton[][]
{
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Доллар 🇺🇸","usd"),
                    InlineKeyboardButton.WithCallbackData("Рубль 🇷🇺","rub"),
                    InlineKeyboardButton.WithCallbackData("Сум 🇺🇿","sum")
                },


                new []
                {
                    InlineKeyboardButton.WithCallbackData("Главное меню 🔠","menu")
                }
});

        public static InlineKeyboardMarkup menuMarkup =
            new InlineKeyboardMarkup(new InlineKeyboardButton[][]
{
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Команды 🏦","commands"),
                    InlineKeyboardButton.WithCallbackData("Помощь 🤝","help")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Ввод покупок ⤵️","input"),
                    InlineKeyboardButton.WithCallbackData("Вывод покупок ⤴️","output")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Настройки ⚙️","setting")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Главное меню 🔠","menu")
                }
});

        public static InlineKeyboardMarkup commandMarkup =
            new InlineKeyboardMarkup(new InlineKeyboardButton[][]
{
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Главное меню 🔠","menu")
                }
});

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

            await botClient.SendTextMessageAsync(e.Message.Chat, commandMessage, 
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                replyMarkup: menuMarkup);
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
            await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: replyMessage,
                replyMarkup: force,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }

        public async static void GetExpense(MessageEventArgs e, ITelegramBotClient botClient)
        {
            if (File.Exists(@"../../../data/tables/" + e.Message.Chat.Id + ".csv"))
            {
                string expenses = ExpensesTable.ReadCSV(e.Message.Chat.Id.ToString());
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: expenses);
            }
            else
            {
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: noJsonMessage,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
            }
        }
        public async static void FillExpense(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                string[] text = Regex.Replace(e.Message.Text, @"\s+", " ").Split(' ');

                if (MessageParser.CheckMessage(text))
                {
                    ExpensesTable.WriteCSV(
                        id: e.Message.Chat.Id.ToString(),
                        obj: string.Join(" ", text.Take(text.Length - 2)),
                        price: double.Parse(text[text.Length - 2]),
                        currency: text[text.Length - 1]);

                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "*Успешно добавлено ✅*\n\n" + replyMessage,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: force
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
                        text: "*Некорректные данные о покупке 🥴*\n\n" + replyMessage,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: force
                        );
            }
        }

        public async static void ShowError(MessageEventArgs e, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: "Что-то пошло не так! Введите правильно покупку " +
                $"или используйте команды для общения с ботом.{newLine}__φ(．．).");
        }
        //public async static void SendCurrency(MessageEventArgs e)
        //{
        //    await botClient.SendTextMessageAsync(e.Message.Chat, $"Текущий курс $USD {CurrencyParser.getCurrency()}");
        //}

        //public async static void SendSticker(MessageEventArgs e)
        //{
        //    await botClient.SendStickerAsync(e.Message.Chat, "CAADAgADrwAD3IxiDDHx46SlmVsxFgQ");
        //}
    }
}
