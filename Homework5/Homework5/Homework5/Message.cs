using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5
{
    public class Message
    {
        string _author, _addressee, _text;
        public Message(string author, string addressee, string text)
        {            
            _author = author;
            _addressee = addressee;
            _text = text;
        }

        public void SetText(string text)
        {
            _text = text;
        }
        public string GetText()
        {
            return _text;
        }

        public void SetAddressee(string addressee)
        {
            _addressee = addressee;
        }
        public string GetAddressee()
        {
            return _addressee;
        }

        public void SetAuthor(string author)
        {
            _author = author;
        }

        public string GetAuthor()
        {
            return _author;
        }
    }
}
