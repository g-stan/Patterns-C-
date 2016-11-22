using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5.Decorator
{
    public class ChatClient : IChatClient
    {
        public void SendMessage(Message message)
        {
            //Отправка сообщения
            //...
        }
        public Message ReceiveMessage(Message message)
        {
            //Получение сообщения
            //...
            return message;
        }
    }
}
