using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Text.RegularExpressions;

namespace BotLibrary
{
    public class CommandHandler
    {
        private static Telegram.Bot.Types.ReplyMarkups.ForceReplyMarkup force =
            new Telegram.Bot.Types.ReplyMarkups.ForceReplyMarkup();
        private static string newLine = Environment.NewLine;
        public const string replyMessage = "Введите Вашу покупку в виде:\n" +
            "`{Предмет} {Цена} {Валюта}`\n\n`/help` для помощи...";


        public async static void DoStart(MessageEventArgs e, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(e.Message.Chat,
                $"*Здравствуйте, Вас приветствует Карманный Бухгалтер.*{newLine}" +
                "Введите в текстовое поле просто `/` или `/commands`, " +
                "чтобы увидеть комманды, которые я поддерживаю 😁",
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }

        public async static void ShowCommands(MessageEventArgs e, ITelegramBotClient botClient)
        {

            await botClient.SendTextMessageAsync(e.Message.Chat, $"*Доступные комманды:* {newLine}" +
                $"`/start` - _Показывает приветственное сообщение._{newLine}" +
                $"`/commands` - _Показывает все поддерживаемые комманды._{newLine}" +
                $"`/help` - _Вспомогательная информация._{newLine}" +
                $"`/add_expense` - _Позволяет добавить или дополнить статью расходов._ {newLine}" +
                $"`/get_expense` - _Возвращает сводку расходов._ {newLine}" +
                $"`soon...`", parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }

        public async static void ShowHelp(MessageEventArgs e, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(e.Message.Chat,
                text: $"_Скоро здесь будет помощь..._\n\n" +
            $"Пример: _Резиновый шланг 100 рублей_\n\n" +
            "*ВАЖНО!* Цена должна быть в цифрах и должна идти в конце вместе с валютой.",
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }

        public async static void AddExpense(MessageEventArgs e, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: replyMessage,
                replyMarkup: force,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }

        public async static void FillExpense(MessageEventArgs e, ITelegramBotClient botClient)
        {
            double price;
            string[] text = Regex.Replace(e.Message.Text, @"\s+", " ").Split(' ');
            try
            {
                double.Parse(text[text.Length - 2]);
                if (double.TryParse(text[text.Length - 2], out price) && MessageParser.CheckMessage(text))
                {
                    ExpensesTable.AppendCSV(e.Message.Chat.Id);

                    await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: "Успешно добавлено!");
                }
                else
                {
                    throw new BotException();
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
