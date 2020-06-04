using System;
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
    /// <summary>
    /// Если пользователь отправил сообщение, то обработчик, в зависимости от текста,
    /// с помощью методов из данного класса ответит пользователю.
    /// </summary>
    public class CallbackHandler
    {
        /// <summary>
        /// Метод для отправки сообщения о помощи с вводом.
        /// </summary>
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

        /// <summary>
        /// Метод для отправки сообщения с информацией полезности бота.
        /// </summary>
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

        /// <summary>
        /// Метод для отправки сообщения с информацией, зачем же нужен бот.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
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

        /// <summary>
        /// Метод для отправки сообщения о том, как работает бот.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
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

        /// <summary>
        ///  Метод для отправки сообщения с кнопками главного меню.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
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

        /// <summary>
        /// Метод для отправки сообщения с информацией о настройках и выбором языки по-умолчанию.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
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

        /// <summary>
        /// Метод для отправки сообщения с выбором кнопок подсказок.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
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

        /// <summary>
        /// Метод для отправки сообщения с выбором фильтрации.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
        public async static void FilterShow(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
                await botClient.AnswerCallbackQueryAsync
                               (e.CallbackQuery.Id);

                await botClient.EditMessageTextAsync(
                    chatId: chatID,
                    text: filterMessage,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    messageId: e.CallbackQuery.Message.MessageId,
                    replyMarkup: filterMarkup);
            }
            catch
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }

        /// <summary>
        /// Метод для отправки сообщения с запросом о вводе информации.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
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

        /// <summary>
        /// Метод для отправки сообщения с информацией о покупках.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
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
                        + stringBuilder.ToString() + "\n*Так же Вы можете получить анализ, " +
                        "нажав на соответствующую кнопку ниже 📊*",
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

        /// <summary>
        /// Метод для отправки сообщения с информацией о покупках за сегодняшний день.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
        public async static void OutputTodayShow(CallbackQueryEventArgs e, ITelegramBotClient botClient)
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
                    var purchasesToday = purchases.Where(ex => ex.Date.Day == DateTime.Now.Day).ToList();

                    if (purchasesToday.Count != 0)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        for (int i = 0; i < purchasesToday.Count; i++)
                        {
                            stringBuilder.AppendLine($"{i + 1}. " + purchasesToday[i].ToString());
                        }
                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: $"*Ваши покупки вида за сегодня 📝:*\n" +
                            $"_Название Цена Валюта Категория Дата_\n\n"
                            + stringBuilder.ToString() + "\n*Так же Вы можете получить анализ, " +
                            "нажав на соответствующую кнопку ниже 📊*",
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: filterTodayMarkup);
                    }
                    else
                    {
                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: $"*Хммм, произошла ошибочка 🤨*\n"+
                            "*Похоже, у Вас нет покупок за сегодня 📝*\n\n" +
                            $"Попробуйте воспользоваться командой /add\\_expense, чтобы их добавить 🙃",
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: filterMarkup);
                    }
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

        /// <summary>
        /// Метод для отправки сообщения с информацией о покупках за текущий месяц.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
        public async static void OutputMonthShow(CallbackQueryEventArgs e, ITelegramBotClient botClient)
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
                    var purchasesMonth = purchases.Where(ex => ex.Date.Month == DateTime.Now.Month).ToList();

                    if(purchasesMonth.Count != 0)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        for (int i = 0; i < purchasesMonth.Count; i++)
                        {
                            stringBuilder.AppendLine($"{i + 1}. " + purchasesMonth[i].ToString());
                        }
                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: $"*Ваши покупки за месяц вида 📑:*\n" +
                            $"_Название Цена Валюта Категория Дата_\n\n"
                            + stringBuilder.ToString() + "\n*Так же Вы можете получить анализ, " +
                            "нажав на соответствующую кнопку ниже 📊*",
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: filterMonthMarkup);
                    }
                    else
                    {
                        await botClient.EditMessageTextAsync(
                            chatId: chatID,
                            text: $"*Хммм, произошла ошибочка 🤨*\n" +
                            "*Похоже, у Вас нет покупок за этот месяц 📑*\n\n" +
                            $"Попробуйте воспользоваться командой /add\\_expense, чтобы их добавить 🙃",
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            messageId: e.CallbackQuery.Message.MessageId,
                            replyMarkup: filterMarkup);
                    }
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

        /// <summary>
        ///  Метод для отправки сообщения с целью.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
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
                    $"До цели ➡ *{goal.GoalPrice} {goal.GoalCurrency}*",
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

        /// <summary>
        /// Метод для отправки сообщения с информацией о командах.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
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

                // Считаем предпочтительную валюту пользователя.
                var currency = User.ReadJSON(chatID).PreferableCurrency;

                bool hasUSD = false;
                bool hasRUB = false;
                RatesResults USD = null;
                RatesResults RUB = null;

                foreach (var purchase in purchasesList)
                {
                    if (purchase.Currency == "USD" && currency == "RUB" && hasUSD==false)
                    {
                        USD = CurrencyParser.GetRates("USD");
                        hasUSD = true;
                    }
                    else if (purchase.Currency == "RUB" && currency == "USD" && hasRUB == false)
                    {
                        RUB = CurrencyParser.GetRates("RUB");
                        hasRUB = true;
                    }

                }

                foreach (var purchase in purchasesList)
                {
                    if (purchase.Currency == "USD" && currency == "RUB")
                    {
                        if(USD!=null)
                            purchase.Price *= USD.conversion_rates.RUB;
                        else
                            purchase.Price *= 73.61;
                    }    

                    else if (purchase.Currency == "RUB" && currency == "USD")
                    {
                        if (RUB != null)
                            purchase.Price *= RUB.conversion_rates.USD;
                        else
                            purchase.Price *= 0.014;
                    }

                    else if (purchase.Currency == "USD" && currency == "UZS")
                        purchase.Price *= 10110.00;

                    else if (purchase.Currency == "UZS" && currency == "USD")
                        purchase.Price *= 0.000099;

                    else if (purchase.Currency == "RUB" && currency == "UZS")
                        purchase.Price *= 137.34;

                    else if (purchase.Currency == "UZS" && currency == "RUB")
                        purchase.Price *= 0.0073;
                }

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
                    caption: "Вот *График* Ваших расходов по датам:",
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                }
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("[Markup] Error");
            }
        }

        /// <summary>
        /// Метод для вывода круговой диаграммы расходов в чат.
        /// Использует System.Design для отрисовки Bitmap.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="botClient"></param>
        public async static void PieShow(CallbackQueryEventArgs e, ITelegramBotClient botClient)
        {
            try
            {
                var chatID = e.CallbackQuery.Message.Chat.Id;
                await botClient.AnswerCallbackQueryAsync
                                (e.CallbackQuery.Id);

                // Считывание всех покупок из JSON. Затем сортировка по дате и суммирование цен.
                var purchasesList = PurchaseInfo.ReadPurchase(chatID);

                // Группируем элементы в подсписки по категории покупок.
                var purchasesGrouped = purchasesList.GroupBy(el => el.Type);
                
                // Из сгруппированных списков извлекаем только значения ключа, по которому группировали.
                var categoriesGroup = purchasesGrouped.Select(el => el.Key).ToList();

                // Из сгруппированных списков извлекаем сумму всех покупок этого типа.
                var categoriesCount = purchasesGrouped.Select(el => el.Count()).ToList();

                // Соединяем список типов и сумму покупок этих типов.
                var categoriesPercentage =
                    categoriesGroup.Zip(categoriesCount, (a, b) => new { a, b }).OrderByDescending(el => el.b).ToList();

                // Вызываем метод, который сохранит круговую диаграмму.
                Analysis.PieAnalysis(categoriesPercentage.Select(el => el.b).ToList(),
                    categoriesPercentage.Select(el => el.a).ToList(),
                    chatID);

                // С помощью потока загружаем изображение в чат.
                using (var stream = File.Open($"../../../data/pies/{chatID}.png", FileMode.Open))
                {
                    var file = new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream);
                    file.FileName = "Pie";
                    await botClient.SendPhotoAsync(chatId: chatID,
                    photo: file,
                    caption: "Вот *Круговая Диаграмма* Ваших расходов по категориям:",
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
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
