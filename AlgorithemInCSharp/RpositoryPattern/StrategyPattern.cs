namespace AlgorithemInCSharp.RpositoryPattern
{
    public enum Country
    {
        USA,
        UAE,
        LUX,
        German,
    }
    public interface ITaxStartegy
    {
        decimal Calculate(decimal price);
    }

    public class USATax : ITaxStartegy
    {
        public decimal Calculate(decimal price)
        {
            decimal res = price + price * -0.9m;
            return res;
        }
    }

    public class UAETax : ITaxStartegy
    {
        public decimal Calculate(decimal price)
        {
            decimal res = price + price * +1.1m;
            return res;

        }
    }

    public class GermanTax : ITaxStartegy
    {
        public decimal Calculate(decimal price)
        {
            decimal res = price + price *  +1.8m;
            return res;
        }
    }

    public class ClientTax
    {
        private readonly ITaxStartegy _startegy;
        public ClientTax(ITaxStartegy startegy)
        {
            _startegy = startegy;
        }

        public decimal CalTax(decimal price)
        {
            var result = _startegy.Calculate(price);
            return result;
           
        }
    }


    public class TaxStrategyFactory
    {
        public ITaxStartegy GetTaxStrategy(Country country)
        {
            switch (country)
            {
                case Country.UAE:
                    return new UAETax();
                case Country.USA:
                    return new USATax();
                case Country.German:
                    return new GermanTax();
                default:
                    throw new NotImplementedException();
            }
                
        }
    }

}