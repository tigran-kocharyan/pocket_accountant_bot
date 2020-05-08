using Telegram.Bot.Types.ReplyMarkups;

namespace BotLibrary
{
    public class Markups
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

        public static InlineKeyboardMarkup analysisMarkup =
            new InlineKeyboardMarkup(new InlineKeyboardButton[][]
{
                new []
                {
                    InlineKeyboardButton.WithCallbackData("График расходов 📈","graphic")
                }
});

    }
}
