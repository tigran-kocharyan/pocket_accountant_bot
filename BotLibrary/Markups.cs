using Telegram.Bot.Types.ReplyMarkups;

namespace BotLibrary
{
    /// <summary>
    /// Класс, который хранит необходимые ReplyMarkup, 
    /// которые прикрепляются к сообщению.
    /// </summary>
    public class Markups
    {
        /// <summary>
        /// Отображение интерфейса ответа для пользователя.
        /// </summary>
        public static ForceReplyMarkup force =
            new ForceReplyMarkup();

        /// <summary>
        /// Кнопки для меню помощи
        /// </summary>
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

        /// <summary>
        /// Кнопки настроек
        /// </summary>
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

        /// <summary>
        /// Кнопки для главного меню.
        /// </summary>
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

        /// <summary>
        /// Кнопка для сообщения комманд.
        /// </summary>
        public static InlineKeyboardMarkup commandMarkup =
            new InlineKeyboardMarkup(new InlineKeyboardButton[][]
{
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Главное меню 🔠","menu")
                }
});

        /// <summary>
        /// Кнопка для реализации анализа.
        /// </summary>
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

        /// <summary>
        /// Кнопки для фильтрации.
        /// </summary>
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

        /// <summary>
        /// Кнопки для сообщения с покупками за сегодняшний день.
        /// </summary>
        public static InlineKeyboardMarkup filterTodayMarkup =
            new InlineKeyboardMarkup(new InlineKeyboardButton[][]
{
                new []
                {
                    InlineKeyboardButton.WithCallbackData("За этот месяц 📑","month"),
                    InlineKeyboardButton.WithCallbackData("За всё время 🛒","alltime")
                },
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
                    InlineKeyboardButton.WithCallbackData("Главное меню 🔠","menu")
                }
});

        /// <summary>
        /// Кнопки для фильтрации.
        /// </summary>
        public static InlineKeyboardMarkup filterMonthMarkup =
            new InlineKeyboardMarkup(new InlineKeyboardButton[][]
{
                new []
                {
                    InlineKeyboardButton.WithCallbackData("За Сегодня 📝","today"),
                    InlineKeyboardButton.WithCallbackData("За всё время 🛒","alltime")
                },
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
                    InlineKeyboardButton.WithCallbackData("Главное меню 🔠","menu")
                }
});

        /// <summary>
        /// Кнопки для изменения цели.
        /// </summary>
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
