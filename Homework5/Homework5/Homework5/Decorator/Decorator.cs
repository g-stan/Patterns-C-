using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5.Decorator
{
    public class ChatDecoratorBase : IChatClient
    {
        protected readonly IChatClient Decoratee;

        protected ChatDecoratorBase(IChatClient chatClient)
        {
            Decoratee = chatClient;
        }

        public void SendMessage(Message message)
        {
            message = OnBeforeSendMessage(message);
            Decoratee.SendMessage(message);
            OnAfterSendMessage(message);
        }

        public Message ReceiveMessage(Message message)
        {
            message = OnBeforeReceiveMessage(message);
            var result = Decoratee.ReceiveMessage(message);
            return OnAfterReceiveMessage(result);
        }

        protected virtual Message OnBeforeSendMessage(Message message)
        {
            return message;
        }

        protected virtual Message OnAfterSendMessage(Message message)
        {
            return message;
        }

        protected virtual Message OnBeforeReceiveMessage(Message message)
        {
            return message;
        }

        protected virtual Message OnAfterReceiveMessage(Message message)
        {
            return message;
        }
    }

    public class UserHidingDecorator : ChatDecoratorBase
    {
        public UserHidingDecorator(IChatClient chatClient) : base(chatClient)
        { }

        protected override Message OnBeforeSendMessage(Message message)
        {            
            message.SetAuthor(Encrypt(message.GetAuthor()));                        
            message.SetAddressee(Encrypt(message.GetAddressee()));

            Console.WriteLine("Encrypted Author: \"{0}\" \n Encrypted Addressee \"{1}\"", message.GetAuthor(), message.GetAddressee());
            return base.OnBeforeSendMessage(message);
        }

        protected override Message OnAfterReceiveMessage(Message message)
        {
            message.SetAuthor(Decrypt(message.GetAuthor()));
            message.SetAddressee(Decrypt(message.GetAuthor()));
            Console.WriteLine("Decrypted Author: \"{0}\" \n Decrypted Addressee \"{1}\"", message.GetAuthor(), message.GetAddressee());
            return base.OnBeforeSendMessage(message);
        }

        public string Encrypt(string originalText)
        {
            var byteArrayMess = Encoding.Unicode.GetBytes(originalText);
            for (var i = 0; i < byteArrayMess.Count(); i++)
            { byteArrayMess[i] -= 1; }
            return (Encoding.Unicode.GetString(byteArrayMess));

        }

        public string Decrypt(string changedText)
        {
            var byteArrayMess = Encoding.Unicode.GetBytes(changedText);
            for (var i = 0; i < byteArrayMess.Count(); i++)
            { byteArrayMess[i] += 1; }
            return (Encoding.Unicode.GetString(byteArrayMess));

        }
    }

    public class EncryptDecryptDecorator : ChatDecoratorBase
    {
        public EncryptDecryptDecorator(IChatClient chatClient) : base(chatClient)
        { }

        protected override Message OnBeforeSendMessage(Message message)
        {
            message.SetText(String.Concat("<encrypt>", message.GetText() , "<encrypt>"));
            Console.WriteLine("Encrypted Text: \"{0}\"", message.GetText());
            return base.OnBeforeSendMessage(message);
        }

        protected override Message OnAfterReceiveMessage(Message message)
        {
            message.SetText(message.GetText().Replace("<encrypt>", ""));
            Console.WriteLine("Decrypted Text: \"{0}\"", message.GetText());
            return base.OnBeforeSendMessage(message);
        }
    }
}
