using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Telegram_Bot.Commands
{
    interface ICommand
    {
        void RunCommand(ref TelegramBotClient bot, Telegram.Bot.Types.Message message);
    }
}
