﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using System.ComponentModel;
using Telegram_Bot.Commands;

namespace Telegram_Bot
{
    class TelegramBot
    {
        private List<long> ApprovedUsersList;

        private string KEY = "1649996386:AAFw4DkRrQJEekekofrYPdt0NyNAoNED4vM";
        private string ApprovedUsersListPath = "ApprovedUsersList.txt";

        private Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>
        {
            ["/crypto"] = new Crypto(),
            ["/addUser"] = new AddUser(),
            ["/exchange"] = new ExchangeCurrency(),
            ["/bulkMail"] = new BulkMail()
            // Добавить остальные функции
        };
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
        private void AddUser(long id)
        {
            ApprovedUsersList.Add(id);
            string output = "";
            output += ApprovedUsersList[0];
            for (int i = 1; i < ApprovedUsersList.Count; ++i)
                output += '\n' + ApprovedUsersList[i].ToString();
            File.WriteAllText(ApprovedUsersListPath, output);
        }
        private async void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                await bot.SetWebhookAsync("");

                bot.OnUpdate += async (object obj, Telegram.Bot.Args.UpdateEventArgs uea) =>
                {
                    var message = uea.Update.Message;
                    Console.WriteLine($"Name:{message.From.FirstName} id:{message.From.Id}  {message.Text}");

                    if (message == null) return;
                    if (!ApprovedUsersList.Contains(message.From.Id))
                    {
                        if (message.Text == "/addUser")
                        {
                            AddUser(message.From.Id);
                            bot.SendTextMessageAsync(message.Chat.Id, $"Welcome to the club, {message.From.FirstName}", replyToMessageId: message.MessageId);
                        }
                        else 
                            bot.SendTextMessageAsync(message.Chat.Id, "Вы не авторизованы для работы в этом чате", replyToMessageId: message.MessageId);
                        return;
                    }
                    
                    
                    if (message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                        Commands[message.Text.Split()[0]].RunCommand(ref bot, message);
                
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