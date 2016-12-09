using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework7
{
    class Program
    {
        static void Main(string[] args)
        {
            var duplicator1 = new Duplicator();
            duplicator1.ReceiveMoney(50);
            duplicator1.ChooseDevice();
            duplicator1.ChooseDocument();
            duplicator1.PrintDocument();
            duplicator1.CloseSession();

            try
            {
                var duplicator2 = new Duplicator();
                duplicator2.ReceiveMoney(100);
                duplicator2.ChooseDevice();
                duplicator2.ChooseDevice();
                duplicator2.PrintDocument();
            }
            catch(Exception e)
            {
                Console.WriteLine($"duplicator2: {e.Message}");
            }

            Console.Read();
        }
    }
}
