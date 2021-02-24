using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using HtmlAgilityPack;

namespace Telegram_Bot.Commands
{
    public struct Currency
    {
        public string Name { get; }
        public string Value { get; }
        public string DailyChange { get; }

        public Currency(string _name, string _value, string _dailyChange)
        {
            Name = _name;
            Value = _value;
            DailyChange = _dailyChange;
        }
    }

    class Crypto : ICommand
    {
        public void RunCommand(ref TelegramBotClient bot,Telegram.Bot.Types.Message message)
        {
            bot.SendTextMessageAsync(message.Chat.Id, $"{ParseTop3Crypto()}", replyToMessageId: message.MessageId);
        }
        private string ParseTop3Crypto()
        {
            List<Currency> result = CurrencyParser.Parse(3);
            string resultString = $"Вот топ-3 криптовалют по стоимости за {DateTime.Now}";
            resultString += "\n_____________________________________";
            for (int i = 0; i < result.Count; ++i)
                resultString += $"\n#{i + 1}: {result[i].Name} {result[i].Value}$ ({result[i].DailyChange} за день)\n";
            resultString += "_____________________________________";
            resultString += "\nУдачи на полях электронных баталий :)";
            return resultString;
        }
    }
    static public class CurrencyParser
    {
        static private HtmlDocument html;
        static public List<Currency> Parse(int limit)
        {
            html = new HtmlWeb().Load("https://ru.investing.com/crypto/");
            HtmlNodeCollection collection = html.DocumentNode.SelectNodes("//table[contains(@class, 'genTbl js-top-crypto-table mostActiveStockTbl crossRatesTbl allCryptoTlb wideTbl elpTbl elp15 ')]//tbody//tr"); // respTbl
            List<Currency> result = new List<Currency>((collection.Count < limit) ? collection.Count : limit);

            string currencyName;
            string currencyValue;
            string currencyDailyChange;

            int maxValue = -1;
            for (int i = 0; i < result.Capacity; ++i)
            {
                currencyName = collection[i].SelectSingleNode("td[contains(@class, 'left bold elp name cryptoName first js-currency-name')]").GetAttributeValue("title", "Error");
                currencyValue = collection[i].SelectSingleNode("td[contains(@class, 'price js-currency-price')]//a[contains(@class, '')]").GetDirectInnerText();
                currencyDailyChange = collection[i].SelectSingleNode("td[contains(@class, 'js-currency-change-24h')]").GetDirectInnerText();

                result.Add(new Currency(currencyName, currencyValue, currencyDailyChange));
            }
            return result;
        }

    }

}
