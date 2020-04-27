using System;
using System.Linq;
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
            "`{Предмет} {Цена} {Валюта}`\n\n`/help` для помощи...";
        public const string replyCheck = "Введите Вашу покупку в виде:\n" +
            "{Предмет} {Цена} {Валюта}\n\n/help для помощи...";

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
            await botClient.SendTextMessageAsync(e.Message.Chat,
                $"*Здравствуйте✌️\nВас приветствует Карманный Бухгалтер 🤖*\n" +
                $"Я помогу Вам управлять расходами 📝\n" +
                "Для начала воспользуйтесь командой `/commands`, " +
                "чтобы увидеть комманды, которые я поддерживаю 😁",
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);

        }

        public async static void ShowCommands(MessageEventArgs e, ITelegramBotClient botClient)
        {

            await botClient.SendTextMessageAsync(e.Message.Chat, $"*Доступные комманды 🏦:* {newLine}" +
                $"`/start` - _Показывает приветственное сообщение 👋_{newLine}" +
                $"`/commands` - _Показывает все поддерживаемые комманды 📋_{newLine}" +
                $"`/help` - _Вспомогательная информация 🤔_{newLine}" +
                $"`/add_expense` - _Позволяет добавить или дополнить статью расходов 📝_ {newLine}" +
                $"`/get_expense` - _Возвращает сводку расходов 📖_ {newLine}" +
                $"`soon to be added...`", parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }

        public async static void ShowHelp(MessageEventArgs e, ITelegramBotClient botClient)
        {
            string outputMessage = $"Вас приветствует помощник Карманного Бухгалтера 👋🏾\n" +
                $"Выберите в каталоге инетерсующий Вас вопрос 👨🏾‍💻";

            await botClient.SendTextMessageAsync(e.Message.Chat,
                text: outputMessage,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                replyMarkup: helpMarkup);
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
            string expenses = ExpensesTable.ReadCSV(e.Message.Chat.Id.ToString());
            await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: expenses);
        }

        public async static void FillExpense(MessageEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                double price;
                string[] text = Regex.Replace(e.Message.Text, @"\s+", " ").Split(' ');

                double.Parse(text[text.Length - 2]);
                if (double.TryParse(text[text.Length - 2], out price)
                    && MessageParser.CheckMessage(text))
                {
                    ExpensesTable.WriteCSV(
                        id: e.Message.Chat.Id.ToString(),
                        obj: string.Join(" ", text.Take(text.Length - 2)),
                        price: price,
                        currency: text[text.Length - 1]);

                    await botClient.SendTextMessageAsync(e.Message.Chat, "Успешно добавлено!");
                }
                else
                {
                    throw new ArgumentException("Invalid Data!");
                }

            }
            catch (Exception)
            {
                ShowError(e, botClient);
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
