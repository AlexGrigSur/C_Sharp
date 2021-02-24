using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Telegram_Bot.Commands
{
    class AddUser : ICommand
    {
        public void RunCommand(ref TelegramBotClient bot, Telegram.Bot.Types.Message message)
        {
            //AddUser(message.From.Id);
            bot.SendTextMessageAsync(message.Chat.Id, $"Welcome to the club, {message.From.FirstName}", replyToMessageId: message.MessageId);
        }
    }
}
