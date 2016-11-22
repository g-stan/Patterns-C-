using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5.Decorator
{
    public class DecoratorBuilder
    {
        IChatClient _chatClient;
        public DecoratorBuilder(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public DecoratorBuilder WithUserHiding()
        {
            _chatClient = new UserHidingDecorator(_chatClient);
            return this;
        }

        public DecoratorBuilder WithTextEncryption()
        {
            _chatClient = new EncryptDecryptDecorator(_chatClient);
            return this;
        }

        public IChatClient Build()
        {
            return _chatClient;
        }
    }
}
