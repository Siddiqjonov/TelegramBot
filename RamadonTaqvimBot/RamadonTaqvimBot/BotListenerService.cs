using Newtonsoft.Json.Linq;
using RamadonTaqvimBot.Bll.Sevices;
using RamadonTaqvimBot.Dal.BotUserEntity;
using System;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace RamadonTaqvimBot;

public class BotListenerService
{
    private readonly string baseUrl;
    //private readonly HttpClient httpClient;
    private readonly string botToken;
    private readonly TelegramBotClient botClient;
    private readonly IBotUserService botUserService;
    public BotListenerService(IBotUserService botUserService)
    {
        baseUrl = "https://islomapi.uz/api/monthly?region=";
        //httpClient = new HttpClient();
        botToken = "7616022184:AAHDFtulPVMMoOoIBJmBz6sKy7n6o3w3kGg";
        botClient = new TelegramBotClient(botToken);
        this.botUserService = botUserService;
    }
    public async Task StartBot()
    {
        botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync);
        Console.WriteLine("Bot is runing");
        Console.ReadKey();
    }
    private static Dictionary<long, string> userRegions = new();

    private async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
    {
        var regions = new HashSet<string>
        {
            "Andijon", "Buxoro", "Farg'ona", "Jizzax", "Xorazm", "Namangan",
            "Navoiy", "Qashqadaryo", "Samarqand", "Sirdaryo", "Surxondaryo", "Toshkent"
        };

        InlineKeyboardMarkup keyboard = new(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Toshkent"), InlineKeyboardButton.WithCallbackData("Andijon") },
                    new[] { InlineKeyboardButton.WithCallbackData("Namangan"), InlineKeyboardButton.WithCallbackData("Farg'ona") },
                    new[] { InlineKeyboardButton.WithCallbackData("Samarqand"), InlineKeyboardButton.WithCallbackData("Buxoro") },
                    new[] { InlineKeyboardButton.WithCallbackData("Xorazm"), InlineKeyboardButton.WithCallbackData("Navoiy") },
                    new[] { InlineKeyboardButton.WithCallbackData("Qashqadaryo"), InlineKeyboardButton.WithCallbackData("Surxondaryo") },
                    new[] { InlineKeyboardButton.WithCallbackData("Jizzax"), InlineKeyboardButton.WithCallbackData("Sirdaryo") },
                });
        if (update.Type == UpdateType.Message)
        {


            var user = update.Message.Chat;
            var message = update.Message;

            


            if (message.Text == "/start")
            {
                var savingUser = new BotUser()
                {
                    TelegramUserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    UpdatedAt = DateTime.UtcNow,
                };
                
                

                await botUserService.AddUserAsync(savingUser);

                await bot.SendMessage(user.Id, "Assalomu alaykum🖐.\r\nRamadan taqvimi botimizga xush kelibsiz😊", cancellationToken: cancellationToken);

                //await SendStartMenu(bot, user.Id);


                 await bot.SendMessage(user.Id, "Qaysi viloyatning taqvimi kerak?", replyMarkup: keyboard);
                //await Task.Delay(7000);
                //await bot.DeleteMessage(user.Id, promptMessage.MessageId);
                return;

            }

            var iftor = "🌅 Iftorlik duosi";
            var sahar = "🌙 Saxarlik duos";
            var viloyat = "Viloyatni o'zgartirish";

            if (message.Text == "/duolar")
            {
                

                var menu = new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton(iftor),
                        new KeyboardButton(sahar)
                    },
                    [
                        new KeyboardButton(viloyat),
                    ]
                })
                {
                    ResizeKeyboard = true,
                    OneTimeKeyboard = true
                };

                await bot.SendMessage(user.Id, "Menudan duo tanlang" , replyMarkup: menu);

                return;
                
            }
            var iftorDuo = "🌅 Iftorlik duosi:\r\nاللَّهُمَّ لَكَ صُمْتُ وَبِكَ آمَنْتُ وَعَلَيْكَ تَوَكَّلْتُ وَعَلَى رِزْقِكَ أَفْطَرْتُ، فَاغْفِرْ لِي ذُنُوبِي يَا غَفَّارُ مَا قَدَّمْتُ وَمَا أَخَّرْتُ.\r\n\r\nTarjimasi ->  Allohumma laka sumtu va bika aamantu va ’alayka tavakkaltu va ’alaa rizqika aftortu, fag‘firliy zunubiy yaa G‘offaru maa qoddamtu va maa axxortu.\r\n\r\nMa'nosi: Allohim, men Sening roziliging uchun ro‘za tutdim, Senga iymon keltirdim, Senga tavakkul qildim va Sen ato etgan rizq bilan iftorlik qildim. Mening oldingi va keyingi gunohlarimni kechir.\r\n\r\n🗓 @Ramadon_taqvimi_2025_bot";
            var saharDuo = "🌙 Saxarlik duos:\r\nنَوَيْتُ أَنْ أَصُومَ صَوْمَ شَهْرِ رَمَضَانَ مِنَ الْفَجْرِ إِلَى الْمَغْرِبِ، خَالِصًا لِلَّهِ تَعَالَى. اللَّهُ أَكْبَرُ.ُ.ُ \r\n\r\nTarjimasi ->  Navaytu an asuma sovma shahri Ramazona minal fajri ilal mag‘ribi, xolisan lillahi ta’alaa. Allohu akbar.\r\n\r\nMa'nosi: Allohim, men Sening roziliging uchun ro‘za tutdim, Senga iymon keltirdim, Senga tavakkul qildim va Sen ato etgan rizq bilan saxarlik qildim.\r\n\r\n🗓 @Ramadon_taqvimi_2025_bot";
            if (message.Text == iftor)
            {
                await bot.SendMessage(user.Id, iftorDuo);
            }
            else if (message.Text == sahar)
            {
                await bot.SendMessage(user.Id, saharDuo);
            }
            else if (message.Text == viloyat)
            {
                await bot.SendMessage(user.Id, "Qaysi viloyatning taqvimi kerak?", replyMarkup: keyboard);
            }



        }
        else if (update.Type == UpdateType.CallbackQuery)
        {

            InlineKeyboardMarkup Inlinekeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("01"),
                    InlineKeyboardButton.WithCallbackData("02"),
                    InlineKeyboardButton.WithCallbackData("03"),
                    InlineKeyboardButton.WithCallbackData("04"),
                    InlineKeyboardButton.WithCallbackData("05"),
                    InlineKeyboardButton.WithCallbackData("06")
                },
                new[] {
                    InlineKeyboardButton.WithCallbackData("07"),
                    InlineKeyboardButton.WithCallbackData("08"),
                    InlineKeyboardButton.WithCallbackData("09"),
                    InlineKeyboardButton.WithCallbackData("10"),
                    InlineKeyboardButton.WithCallbackData("11"),
                    InlineKeyboardButton.WithCallbackData("12")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("13"),
                    InlineKeyboardButton.WithCallbackData("14"),
                    InlineKeyboardButton.WithCallbackData("15"),
                    InlineKeyboardButton.WithCallbackData("16"),
                    InlineKeyboardButton.WithCallbackData("17"),
                    InlineKeyboardButton.WithCallbackData("18")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("19"),
                    InlineKeyboardButton.WithCallbackData("20"),
                    InlineKeyboardButton.WithCallbackData("21"),
                    InlineKeyboardButton.WithCallbackData("22"),
                    InlineKeyboardButton.WithCallbackData("23"),
                    InlineKeyboardButton.WithCallbackData("24")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("25"),
                    InlineKeyboardButton.WithCallbackData("26"),
                    InlineKeyboardButton.WithCallbackData("27"),
                    InlineKeyboardButton.WithCallbackData("28"),
                    InlineKeyboardButton.WithCallbackData("29"),
                    InlineKeyboardButton.WithCallbackData("30"),
                }
            });


            // Declare userRegions as a static dictionary (outside the method)

            var userId = update.CallbackQuery.From.Id;
            var userData = update.CallbackQuery.Data;

            if (regions.Contains(userData))
            {
                userRegions[userId] = userData; // Store selected region
                await bot.SendMessage(userId, $"Siz {userData} viloyatini tanladingiz 😊");
                await bot.SendMessage(userId, "1-30 mart orasida sanani tanlang:", replyMarkup: Inlinekeyboard);
                return;
            }

            var numbers = new HashSet<string>
            {
                "01", "02", "03", "04", "05", "06", "07", "08", "09", "10",
                "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
                "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"
            };
            if (update.Type == UpdateType.CallbackQuery && !userRegions.ContainsKey(userId))
            {
                await bot.SendMessage(userId, "Iltimos, avval viloyatni tanlang. 😊");
                return;
            }
            if (update.Type == UpdateType.CallbackQuery && userRegions.TryGetValue(userId, out string region) && numbers.Contains(userData))
            {
                

                string url = $"{baseUrl}{region}&month=3";
                using HttpClient client = new();
                string json = await client.GetStringAsync(url);
                JArray data = JArray.Parse(json);

                // 'userData' contains the day selected in the callback, e.g. "01"
                string selectedDay = update.CallbackQuery.Data;

                // Find the data for the selected day using LINQ
                var dayData = data.FirstOrDefault(d =>
                {
                    DateTime dateValue = DateTime.Parse(d["date"].ToString());
                    // Format the day as "dd" (e.g. "01")
                    return dateValue.ToString("dd") == selectedDay;
                });

                if (dayData == null)
                {
                    await bot.SendMessage(userId, "Ma'lumot topilmadi.");
                    return;
                }

                // Parse and format the date without the year (e.g. "1‑mart") using the Uzbek culture
                DateTime date = DateTime.Parse(dayData["date"].ToString());
                string formattedDate = date.ToString("d-MMMM", CultureInfo.GetCultureInfo("uz-UZ"));

                // Get the weekday (assumes the API provides this correctly)
                string weekday = dayData["weekday"].ToString();

                // Format the times to only show hours and minutes
                string suhur = DateTime.Parse(dayData["times"]["tong_saharlik"].ToString()).ToString("HH:mm");
                string maghrib = DateTime.Parse(dayData["times"]["shom_iftor"].ToString()).ToString("HH:mm");

                // Build the message (using HTML formatting if desired)
                string message = $"<{formattedDate}, {weekday}> Ramadan Taqvimi🕋\n\n" +
                                 $"{region} viloyatida Saxarlik 🌙     {suhur}\n" +
                                 $"{region} viloyatida Iftorlik 🌅      {maghrib}\n\n" +
                                 "Duolarni olish uchun /duolar ni bosing\n\n" +
                                 "🗓  @Ramadon_taqvimi_2025_bot";

                await bot.SendMessage(userId, message);
            }
        }
    }
    private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(exception.Message);
    }
}
