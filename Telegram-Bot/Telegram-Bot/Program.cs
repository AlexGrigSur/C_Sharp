﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram_Bot;
using Telegram_Bot.Parsers;

namespace Telegram_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("#2: Ethereum, стоимость: 1.773,11$, Изменения в день: -0,12%".Length);
            ExchangeCurrencyParser.Parse("https://www.bestchange.ru/bitcoin-to-paypal-rub.html");
            Console.WriteLine("\n++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            ExchangeCurrencyParser.Parse("https://www.bestchange.ru/ethereum-to-paypal-rub.html");
            Console.WriteLine("\n++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            ExchangeCurrencyParser.Parse("https://www.bestchange.ru/ethereum-to-sberbank.html");
            //TelegramBot bot = new TelegramBot();
            Console.ReadKey();
        }
    }
}
