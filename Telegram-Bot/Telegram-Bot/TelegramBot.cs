using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using System.ComponentModel;
using Telegram_Bot.Parsers;

namespace Telegram_Bot
{
    class TelegramBot
    {
        private List<long> ApprovedUsersList;

        private string KEY = "1649996386:AAFw4DkRrQJEekekofrYPdt0NyNAoNED4vM";
        private string ApprovedUsersListPath = "ApprovedUsersList.txt";
        private BackgroundWorker BGW;
        private TelegramBotClient bot;
        public TelegramBot()
        {
            BGW = new BackgroundWorker();
            BGW.DoWork += new DoWorkEventHandler(DoWork);

            bot = new TelegramBotClient(KEY);
            ApprovedUsersList = new List<long>();
            GetApprovedUsersList();
            this.BGW.RunWorkerAsync();
            Console.WriteLine("Bot has been started");
            Console.ReadKey();
        }

        private async void GetApprovedUsersList()
        {
            if (File.Exists(ApprovedUsersListPath))
            {
                string[] splitText = File.ReadAllText(ApprovedUsersListPath).Split('\n');
                foreach (string i in splitText)
                    ApprovedUsersList.Add(Convert.ToInt64(i));
            }
        }
        private /*async*/ void AddUser(long id)
        {
            ApprovedUsersList.Add(id);
            string output = "";
            output += ApprovedUsersList[0];
            for(int i=1;i<ApprovedUsersList.Count;++i)
                output += '\n' + ApprovedUsersList[i].ToString();
            File.WriteAllText(ApprovedUsersListPath, output);
        }

        private string ParseTop3Crypto() // TODO: Add async
        {
            List<Currency> result = CurrencyParser.Parse(3);
            string resultString = $"Вот топ-3 криптовалют по стоимости за {DateTime.Now}";
            resultString += "\n_____________________________________"; // TODO: change separate line length (adapt to mobile telegram)
            for (int i=0;i<result.Count;++i)
                resultString += $"\n#{i+1}: {result[i].Name} {result[i].Value}$ ({result[i].DailyChange} за день)\n";
            resultString += "_____________________________________";
            resultString += "\nУдачи на полях электронных баталий :)";
            return resultString;
        }

        private async void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                await bot.SetWebhookAsync("");

                bot.OnUpdate += async (object obj, Telegram.Bot.Args.UpdateEventArgs uea) =>
                {
                    var message = uea.Update.Message;
                    Console.WriteLine($"id:{message.From.Id} {message.Text}");
                    if (message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                        if (message.Text == "/addUser")
                        {
                            AddUser(message.From.Id);
                            await bot.SendTextMessageAsync(message.Chat.Id, $"Welcome to the club, {message.From.FirstName}", replyToMessageId: message.MessageId);
                        }
                    if (!ApprovedUsersList.Contains(message.From.Id))
                    {
                        await bot.SendTextMessageAsync(message.Chat.Id, "Вы не авторизованы для работы в этом чате", replyToMessageId: message.MessageId);
                        return;
                    }
                    if (message == null) return;
                    if (message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                    {
                        if (message.Text == "/hello")
                            await bot.SendTextMessageAsync(message.Chat.Id, $"Hello, {message.From.FirstName}", replyToMessageId: message.MessageId);
                        if (message.Text == "/loh")
                            await bot.SendTextMessageAsync(message.Chat.Id, $"pidr", replyToMessageId: message.MessageId);
                        if (message.Text == "/crypto")
                            await bot.SendTextMessageAsync(message.Chat.Id, $"{ParseTop3Crypto()}", replyToMessageId: message.MessageId);
                    }
                };
                bot.StartReceiving();
            }
            catch
            {
                Console.WriteLine("Error occure. Bot was stopped");
            }
        }
    }
}