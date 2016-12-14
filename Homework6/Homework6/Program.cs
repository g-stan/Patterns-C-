using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example_06.Chain;

namespace Homework6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размер денежных средств, которые хотите получить, и валюту через пробел (RUR - рубли, $ - доллары)");
            var requestedMoney = Console.ReadLine();

            var bankomat = new Bancomat();
            Console.WriteLine(bankomat.GetMoney(requestedMoney));
            Console.ReadLine();
        }
        //комментарий
    }
}
