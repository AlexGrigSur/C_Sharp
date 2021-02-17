using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Telegram_Bot
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
    static public class CurrencyParser
    {
        static private HtmlDocument html;//HtmlDocument();
        static public List<Currency> ParseInfo(int limit)
        {   
            html = new HtmlWeb().Load("https://ru.investing.com/crypto/");
            HtmlNodeCollection collection = html.DocumentNode.SelectNodes("//table[contains(@class, 'genTbl js-top-crypto-table mostActiveStockTbl crossRatesTbl allCryptoTlb wideTbl elpTbl elp15 ')]//tbody//tr"); // respTbl
            List<Currency> result = new List<Currency>((collection.Count < limit)? collection.Count: limit);

            string currencyName;
            string currencyValue;
            string currencyDailyChange;

            for (int i=0;i<result.Capacity;++i)
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
