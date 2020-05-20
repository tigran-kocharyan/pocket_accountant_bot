using Telegram.Bot.Types.ReplyMarkups;

namespace BotLibrary
{
    public class Markups
    {
        public static ForceReplyMarkup force =
            new ForceReplyMarkup();

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
                    InlineKeyboardButton.WithCallbackData("Цель 🏆","goal"),
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
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Круговая Диаграмма Категорий 🧿","pie")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Удалить покупки ❌","deletePurchase"),
                    InlineKeyboardButton.WithCallbackData("Фильтр ✂️","filterPurchase")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Главное меню 🔠","menu")
                }
});

        public static InlineKeyboardMarkup filterMarkup =
            new InlineKeyboardMarkup(new InlineKeyboardButton[][]
{
                new []
                {
                    InlineKeyboardButton.WithCallbackData("За Сегодня 📝","today")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("За этот месяц 📑","month")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("За всё время 🛒","alltime")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Главное меню 🔠","menu")
                }
});

        public static InlineKeyboardMarkup editGoalMarkup =
            new InlineKeyboardMarkup(new InlineKeyboardButton[][]
{
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Добавить в Сбережения ❎","addGoal")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Удалить Цель ❌","deleteGoal")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Главное меню 🔠","menu")
                }
});

    }
}
