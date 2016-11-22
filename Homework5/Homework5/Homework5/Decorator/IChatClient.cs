using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5.Decorator
{
    public interface IChatClient
    {
        void SendMessage(Message message);
        Message ReceiveMessage(Message message);
    }
}
