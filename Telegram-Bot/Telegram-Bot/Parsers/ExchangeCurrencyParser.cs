using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Telegram_Bot.Parsers
{
    public struct ExchangeCurrency
    {
        public string Name { get; }
        public string URL { get; }
        public string Value { get; }
        public string MinimumExchange { get; }
        public string MaximumExchange { get; }


        public ExchangeCurrency(string _name, string _url, string _value, string _minExchange, string _maxExchange)
        {
            Name = _name;
            URL = _url;
            Value = _value;
            MinimumExchange = _minExchange;
            MaximumExchange = _maxExchange;
        }
    }
    static class ExchangeCurrencyParser
    {
        static private HtmlDocument html;
        static public /*List<Currency>*/void Parse(string link, int limit = 3)
        {
            html = new HtmlWeb().Load(link);
            HtmlNodeCollection collection = html.DocumentNode.SelectNodes("//table[contains(@id, 'content_table')]//tbody//tr[contains(@onclick, 'ccl(')]");
            //Console.WriteLine(collection[0].InnerHtml);
            //Console.WriteLine("_______________________________________________");
            //Console.WriteLine(collection[1].InnerHtml);
            List<ExchangeCurrency> result = new List<ExchangeCurrency>((collection.Count < limit) ? collection.Count : limit);

            string currencyName;
            string currencyValue;

            HtmlNodeCollection CurrencyInfoCollection = collection[0].SelectNodes("td[contains(@class, 'bi')]");
            string inputCurrency = CurrencyInfoCollection[0].SelectSingleNode("div[contains(@class, 'fs')]//small").GetDirectInnerText();

            string outputCurrency = CurrencyInfoCollection[1].SelectSingleNode("small").GetDirectInnerText();
            //Console.WriteLine(inputCurrency);
            //Console.WriteLine(outputCurrency);
            //Console.WriteLine(minExchange);
            //Console.WriteLine(maxExchange);
            Console.WriteLine("\n_______________________________________________");
            Console.WriteLine($"Обмен: {inputCurrency} - {outputCurrency}");
            Console.WriteLine("_______________________________________________");
            for (int i = 0; i < result.Capacity; ++i)
            {
                CurrencyInfoCollection = collection[i].SelectNodes("td[contains(@class, 'bi')]"); // tr//

                string ExchangeName = collection[i].SelectSingleNode("td[contains(@class, 'bj')]//div//div//div").GetDirectInnerText(); // tr//
                string Link = collection[i].SelectSingleNode("td[contains(@class, 'bj')]//div//a").GetAttributeValue("href", "Error"); // tr//
                string minExchange = CurrencyInfoCollection[0].SelectSingleNode("div[contains(@class, 'fm')]//div[contains(@class, 'fm1')]").GetDirectInnerText();
                string maxExchange = CurrencyInfoCollection[0].SelectSingleNode("div[contains(@class, 'fm')]//div[contains(@class, 'fm2')]").GetDirectInnerText();
                string ExchangeValue = CurrencyInfoCollection[1].GetDirectInnerText();
                Console.WriteLine("_______________________________________________");
                Console.WriteLine($"Обменник: {ExchangeName}");
                Console.WriteLine($"Ссылка: {Link}");
                Console.WriteLine($"{minExchange} {inputCurrency}");
                Console.WriteLine($"{maxExchange} {inputCurrency}");
                Console.WriteLine($"Цена: 1 {inputCurrency} = {ExchangeValue} {outputCurrency}");

                //result.Add(new Currency(currencyName, currencyValue));
            }
            //return result;
        }

    }
}

