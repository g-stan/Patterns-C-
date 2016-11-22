using Homework5.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5
{
    class Program
    {
        static void Main(string[] args)
        {
            IChatClient chatclient = new ChatClient();

            //цепочка дерорторов
            chatclient = new DecoratorBuilder(chatclient)
                .WithUserHiding()
                .WithTextEncryption()
                .Build();

            var message = new Message(author: "George", addressee: "Alexander", text: "Test. Hello there! How are you?");
            chatclient.SendMessage(message);
            var result = chatclient.ReceiveMessage(message);
            Console.ReadLine();
        }
    }
}
