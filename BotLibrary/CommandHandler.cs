using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotLibrary
{
    public class CommandHandler
    {
        private static string newLine = Environment.NewLine;
        public const string replyMessage = "Введите Вашу покупку в виде:\n" +
            "_Предмет Цена Валюта_\n_Мозг админу 999 рублей_\n\n/help для помощи...";
        public const string replyCheck = "Введите Вашу покупку в виде:\n" +
            "Предмет Цена Валюта\nМозг админу 999 рублей\n\n/help для помощи...";

        public static Telegram.Bot.Types.ReplyMarkups.ForceReplyMarkup force =
            new Telegram.Bot.Types.ReplyMarkups.ForceReplyMarkup();

        public static InlineKeyboardMarkup helpMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
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
                }
});

        public async static void DoStart(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                await botClient.SendTextMessageAsync(e.Message.Chat,
                    $"*Здравствуйте✌️\nЯ - Карманный Бухгалтер 🤖*\n\n" +
                    $"Я помогу Вам управлять расходами 📝\n" +
                    "Для начала воспользуйтесь командой /commands, " +
                    "чтобы увидеть комманды, которые я поддерживаю 😁",
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{new String('=', 30)}\nERROR: {ex.Message}");
            }
        }

        public async static void ShowCommands(MessageEventArgs e, ITelegramBotClient botClient)
        {

            await botClient.SendTextMessageAsync(e.Message.Chat, $"*Доступные комманды 🏦:*\n" +
                $"/start - _Показывает приветственное сообщение 👋_\n\n" +
                $"/commands - _Показывает все поддерживаемые комманды 📋_\n\n" +
                $"/help - _Вспомогательная информация 🤔_\n\n" +
                $"/add\\_expense - _Позволяет добавить или дополнить статью расходов 📝_\n\n" +
                $"/get\\_expense - _Возвращает сводку расходов 📖_\n" +
                $"_soon to be added..._", parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }

        public async static void ShowHelp(MessageEventArgs e, ITelegramBotClient botClient)
        {
            string outputMessage = $"Вас приветствует помощник Карманного Бухгалтера 👋🏾\n" +
                $"Выберите инетерсующий Вас вопрос 👨🏾‍💻";

            try
            {
                await botClient.SendTextMessageAsync(e.Message.Chat,
                text: outputMessage,
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
            if (File.Exists(@"../../../data/tables/"+ e.Message.Chat.Id + ".csv"))
            {
                string expenses = ExpensesTable.ReadCSV(e.Message.Chat.Id.ToString());
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: expenses);
            }
            else
            {
                string expenses = "*Хах, произошла ошибочка 😅*\n\n" +
                    "Странно, что Вы пытаетесь получить расходы, не добавив их. " +
                    "Воспользуйтесь /add\\_expense или просто введите покупку 👨🏾‍💻";
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: expenses,
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
