using System;
using System.Collections;

namespace Example_06.Chain
{
    public enum CurrencyType
    {
        Dollar,
        RUR,
        Unknown
    }

    public class Money
    {
        public CurrencyType Currency { get; }
        public string Value { get; set; }
        public string BanknotesToUser { get; set; }

        public Money(string currencyType, string val)
        {
            switch(currencyType)
            {
                case "RUR":
                    Currency = CurrencyType.RUR;
                    break;
                case "$":
                    Currency = CurrencyType.Dollar;
                    break;
                default:
                    Currency = CurrencyType.Unknown;
                    break;
            }
            
            Value = val;
            BanknotesToUser = "";
        }
    }

    public class Bancomat
    {
        private BanknoteHandler _handler;

        public Bancomat()
        {
        }
                
        public string GetMoney(string requestedMoney)
        {
            string[] moneyMasStr = requestedMoney.Split(' ');
            if (moneyMasStr.Length > 2) return ("Некорректно введена запрашиваемая сумма");
            int requestedMoneyInt;
            var isMoneyParsed = int.TryParse(moneyMasStr[0], out requestedMoneyInt);
            var money = new Money(moneyMasStr[1], moneyMasStr[0]);

            _handler = new TenRubleHandler(null);
            _handler = new FiftyRubleHandler(_handler);
            _handler = new HundredRubleHandler(_handler);
            _handler = new FiveHundredRubleHandler(_handler);
            _handler = new ThousandRubleHandler(_handler);
            _handler = new FiveThousandRubleHandler(_handler);
            _handler = new TenDollarHandler(_handler);
            _handler = new FiftyDollarHandler(_handler);
            _handler = new HundredDollarHandler(_handler);
            _handler = new FiveHundredDollarHandler(_handler);
            _handler = new InvalidValueHandler(_handler);

            _handler.CalculateBanknotes(ref money);
            return money.BanknotesToUser;
        } 
    }

    public abstract class BanknoteHandler
    {
        private readonly BanknoteHandler _nextHandler;

        //Разряд числа; не используется
        protected abstract int Category { get; }

        protected BanknoteHandler(BanknoteHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public virtual void CalculateBanknotes(ref Money requestedMon)
        {
             if(_nextHandler != null) _nextHandler.CalculateBanknotes(ref requestedMon);
        }

        public void Calculate(ref Money mon)
        {
            
        }
    }

    public class InvalidValueHandler : BanknoteHandler
    {
        protected override int Category => 1;
        public override void CalculateBanknotes(ref Money requestedMon)
        {
            if (requestedMon.Value.EndsWith("0") && requestedMon.Value !="0")
            { base.CalculateBanknotes(ref requestedMon); }
            else { requestedMon.BanknotesToUser = "В банкомате нет подходящих купюр"; }            
        }

        public InvalidValueHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }
    public abstract class RubleHandlerBase : BanknoteHandler
    {
        public override void CalculateBanknotes(ref Money requestedMon)
        {
            if (requestedMon.Currency.Equals(CurrencyType.RUR))
            {
                var requestedMonInt = int.Parse(requestedMon.Value);
                var banknoteCount = requestedMonInt / Value;
                if (banknoteCount>0) requestedMon.BanknotesToUser += $" {banknoteCount}*{Value}";
                requestedMon.Value = (requestedMonInt - banknoteCount * Value).ToString();
            }
            base.CalculateBanknotes(ref requestedMon);
        }

        protected abstract int Value { get; }

        protected RubleHandlerBase(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class FiveThousandRubleHandler : RubleHandlerBase
    {
        protected override int Value => 5000;
        protected override int Category => 4;
        
        public FiveThousandRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }
    public class ThousandRubleHandler : RubleHandlerBase
    {
        protected override int Value => 1000;
        protected override int Category => 4;
        public ThousandRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }
    public class FiveHundredRubleHandler : RubleHandlerBase
    {
        protected override int Value => 500;
        protected override int Category => 3;
        public FiveHundredRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class HundredRubleHandler : RubleHandlerBase
    {
        protected override int Value => 100;
        protected override int Category => 3;
        public HundredRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class FiftyRubleHandler : RubleHandlerBase
    {
        protected override int Value => 50;
        protected override int Category => 2;
        public FiftyRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class TenRubleHandler : RubleHandlerBase
    {
        protected override int Value => 10;
        protected override int Category => 2;
        public TenRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }
    public abstract class DollarHandlerBase : BanknoteHandler
    {
        public override void CalculateBanknotes(ref Money requestedMon)
        {
            if (requestedMon.Currency.Equals(CurrencyType.Dollar))
            {
                var requestedMonInt = int.Parse(requestedMon.Value);
                var banknoteCount = requestedMonInt / Value;
                if (banknoteCount > 0) requestedMon.BanknotesToUser += $" {banknoteCount}*{Value}";
                requestedMon.Value = (requestedMonInt - banknoteCount * Value).ToString();
            }
            base.CalculateBanknotes(ref requestedMon);
        }

        protected abstract int Value { get; }

        protected DollarHandlerBase(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class FiveHundredDollarHandler : DollarHandlerBase
    {
        protected override int Value => 500;
        protected override int Category => 3;
        public FiveHundredDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class HundredDollarHandler : DollarHandlerBase
    {
        protected override int Value => 100;
        protected override int Category => 3;
        public HundredDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class FiftyDollarHandler : DollarHandlerBase
    {
        protected override int Value => 50;
        protected override int Category => 2;
        public FiftyDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class TenDollarHandler : DollarHandlerBase
    {
        protected override int Value => 10;
        protected override int Category => 2;
        public TenDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }
}
