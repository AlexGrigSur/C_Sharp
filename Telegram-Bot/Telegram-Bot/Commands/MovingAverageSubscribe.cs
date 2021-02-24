using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Telegram_Bot.Commands
{
    class MovingAverageSubscribe : ICommand
    {
        private GreatCryptoMovingAverageParser gcMA;
        public void RunCommand(ref TelegramBotClient bot, Telegram.Bot.Types.Message message)
        {
            gcMA = new GreatCryptoMovingAverageParser();
            while (true)
            {
                CalcMA();
                Thread.Sleep(180000);
            }
        }

        private void CalcMA()
        {
            List<CryptoMovingAverage> toNotifyList;// = gcMA.GetInfo();
        }
    }

    public class CryptoMovingAverage
    {
        public string cryptoName { get; }
        private List<double> last20 = new List<double>(20);
        private double last20Sum = 0;
        private double MA = 0;
        private bool isSubscribersNotified = false;

        public void AddValue(double value)
        {
            if (last20.Count != 20)
                last20Sum += value;
            else
            {
                last20Sum = -last20.First() + value;
                last20.RemoveAt(0);
                last20.Add(value);
            }
        }
        private bool isNotifyRequired()
        {
            MA = last20Sum / 20;
            if (!isSubscribersNotified)
            {
                if (MA > 15)
                {
                    isSubscribersNotified = true;
                    return true;
                }
            }
            else
                if (MA < 12) isSubscribersNotified = false;
            return false;
        }
    }
    public class GreatCryptoMovingAverageParser
    {
        List<CryptoMovingAverage> greatList = new List<CryptoMovingAverage>(5000);
        public void NotifySubscribers()
        {

        }
    }
}
