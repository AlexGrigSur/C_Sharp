using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram_Bot;
using System.ComponentModel;

namespace Telegram_Bot
{
    class Bot
    {
        private string KEY = "1649996386:AAFw4DkRrQJEekekofrYPdt0NyNAoNED4vM";
        private BackgroundWorker BGW;
        private Telegram.Bot.TelegramBotClient BOT;
        public Bot()
        {
            BGW = new BackgroundWorker();
            BGW.DoWork += new DoWorkEventHandler(DoWork);

            BOT = new Telegram.Bot.TelegramBotClient(KEY);
            this.BGW.RunWorkerAsync();
            Console.WriteLine("Bot has been started");
        }

        private async void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                await BOT.SetWebhookAsync("");
                

                BOT.StartReceiving();
            }
            catch
            {
                Console.WriteLine("Error occure. Bot was stopped");
            }
        }
    }
}
