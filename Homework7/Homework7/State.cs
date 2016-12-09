using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework7
{
    public enum DeviceType
    {
        UsbDevice,
        WiFiDevice
    }

    public class Device
    {
        public string Name { get; set; }
        public DeviceType Type { get; set; }

        public Device(string name, DeviceType type)
        {
            Name = name;
            Type = type;
        }
    }

    public class Duplicator
    {
        public Device UserDevice { get; set; }
        public int UserMoney { get; set; }
        public string DocumentName { get; set; }
        public IState State { get; set; }

        public Duplicator()
        {
            UserMoney = 0;
            State = new InitState();
        }

        public void ReceiveMoney(int userMoney)
        {           
            UserMoney += userMoney;
            State.ReceiveMoney(this);
        }
        public void ChooseDevice()
        {
            State.ChooseDevice(this);
        }
        public void ChooseDocument()
        {
            State.ChooseDocument(this);
        }
        public void PrintDocument()
        {
            State.PrintDocument(this);
        }       
        public void CloseSession()
        {
            State.CloseSession(this);
        }
    }

    public interface IState
    {
        void ReceiveMoney(Duplicator duplicator);
        void ChooseDevice(Duplicator duplicator);
        void ChooseDocument(Duplicator duplicator);
        void PrintDocument(Duplicator duplicator);
        void CloseSession(Duplicator duplicator);
    }

    public abstract class StateBase: IState
    {
        public virtual void ReceiveMoney(Duplicator duplicator)
        {
            var banknoteQual = "Ok";
            //Проверка купюры
            //...
            if (banknoteQual == "Ok")
            {
                Console.WriteLine($"Принято {duplicator.UserMoney} рублей");
                duplicator.State = new ChoosingDeviceState();
            }
            else
            {
                Console.WriteLine("Купюра не прошла проверку");
                duplicator.State = new CloseSessionState();
            }
        }
        public abstract void ChooseDevice(Duplicator duplicator);
        public abstract void ChooseDocument(Duplicator duplicator);
        public abstract void PrintDocument(Duplicator duplicator);
        public virtual void CloseSession(Duplicator duplicator)
        {
            duplicator.State = new CloseSessionState();
        }
    }

    public class InitState: StateBase
    {        
        public override void ChooseDevice(Duplicator duplicator)
        {
            throw new Exception("Ошибка. Не внесены денежные средства");
        }

        public override void ChooseDocument(Duplicator duplicator)
        {
            throw new Exception("Ошибка. Не внесены денежные средства");
        }

        public override void PrintDocument(Duplicator duplicator)
        {
            throw new Exception("Ошибка. Не внесены денежные средства");
        }
    }

    public class ChoosingDeviceState: StateBase
    {
        public override void ReceiveMoney(Duplicator duplicator)
        {
            throw new Exception("На данном этапе приём денег не ведётся. Выберите устройство, с которого необходимо печатать");
        }

        public override void ChooseDevice(Duplicator duplicator)
        {
            //Выбор устройства
            //...
            string deviceName = "A";
            DeviceType devType = DeviceType.UsbDevice;
            duplicator.UserDevice = new Device(deviceName, devType);
            duplicator.State = new ChoosingDocumentState();
        }

        public override void ChooseDocument(Duplicator duplicator)
        {
            throw new Exception("Не выбрано устройство, с которого необходимо печатать");
        }

        public override void PrintDocument(Duplicator duplicator)
        {
            throw new Exception("Не выбрано устройство, с которого необходимо печатать");
        }
    }

    public class ChoosingDocumentState : StateBase
    {
        public override void ReceiveMoney(Duplicator duplicator)
        {
            throw new Exception("На данном этапе приём денег не ведётся. Выберите устройство, с которого необходимо печатать");
        }

        public override void ChooseDevice(Duplicator duplicator)
        {
            Console.WriteLine("Возращаемся к экрану выбора устройства");
            duplicator.State = new ChoosingDeviceState();
        }

        public override void ChooseDocument(Duplicator duplicator)
        {
            //Выбор документа
            //...
            duplicator.DocumentName = "Doc1.doc";
            duplicator.State = new PrintDocumentState();
        }

        public override void PrintDocument(Duplicator duplicator)
        {
            throw new Exception("Не выбран документ, который необходимо печатать");
        }
    }

    public class PrintDocumentState : StateBase
    {
        public override void ChooseDevice(Duplicator duplicator)
        {
            throw new Exception("Ошибка. Необходимо вернуться на экран выбора документа");
        }

        public override void ChooseDocument(Duplicator duplicator)
        {
            Console.WriteLine("Возращаемся к экрану выбора документа");
            duplicator.State = new ChoosingDocumentState();
        }

        public override void PrintDocument(Duplicator duplicator)
        {
            //Необходимо вычислить количество страниц и стоимость
            var requerdMoney = 0;
            if (duplicator.UserMoney < requerdMoney)
            {
                Console.WriteLine($"Необходимо внести ещё {requerdMoney - duplicator.UserMoney} рублей");
                duplicator.State = new InitState(); //Пользователь может добавить необходимую сумму или вернуть сдачу 
            }
            else
            {
                //Print
                Console.WriteLine("Print");
                duplicator.UserMoney -= requerdMoney;
                duplicator.State = new ChoosingDocumentState();
            }
        }
    }

    public class CloseSessionState: StateBase
    {
        public override void ReceiveMoney(Duplicator duplicator)
        {
            throw new Exception("Идёт завершение сеанса");
        }
        public override void ChooseDevice(Duplicator duplicator)
        {
            throw new Exception("Идёт завершение сеанса");
        }
        public override void ChooseDocument(Duplicator duplicator)
        {
            throw new Exception("Идёт завершение сеанса");
        }
        public override void PrintDocument(Duplicator duplicator)
        {
            throw new Exception("Идёт завершение сеанса");
        }

        public override void CloseSession(Duplicator duplicator)
        {
            Console.WriteLine($"Ваша сдача {duplicator.UserMoney} рублей");
        }
    }
}
