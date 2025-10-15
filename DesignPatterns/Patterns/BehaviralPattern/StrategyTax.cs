namespace Patterns.BehaviralPattern
{
    public enum Country
    {
        USA,
        German,
        UEA,
        England,
        China,
        Russia,

    };

    public interface ITaxStartegy
    {
        decimal Calculate(decimal price);
    }

    public class USATax : ITaxStartegy
    {
        public decimal Calculate(decimal price)
        {
            return price + price * 0.1m + .9m;
        }
    }

    public class RussiaTax : ITaxStartegy
    {
        public decimal Calculate(decimal price)
        {
            return price + price * .3m + 0.8m;
        }
    }
    public class UEATax : ITaxStartegy
    {
        public decimal Calculate(decimal price)
        {
            return price + price * 0.7m + .4m;
        }
    }

    public class ChinaTax : ITaxStartegy
    {
        public decimal Calculate(decimal price)
        {
            return price + price * 1.1m;
        }
    }

    public class GermanTax : ITaxStartegy
    {
        public decimal Calculate(decimal price)
        {
            return price + price * 1.8m;
        }
    }

    public class EnglandTax : ITaxStartegy
    {
        public decimal Calculate(decimal price)
        {
            return price + price * 1.8m;
        }
    }


    public class TaxStrategyFactory
    {
        public ITaxStartegy GetTaxStrtegy(Country country)
        {
            switch (country)
            {
                case Country.China:
                    return new ChinaTax();
                case Country.England:
                    return new EnglandTax();
                case Country.UEA:
                    return new EnglandTax();
                case Country.German:
                    return new GermanTax();
                case Country.Russia:
                    return new RussiaTax();
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public class ClientTax
    {
        protected readonly ITaxStartegy _taxStartegy;
        public ClientTax(ITaxStartegy taxStartegy)
        {
            _taxStartegy = taxStartegy;
        }
        public decimal CalTax(decimal price)
        {

            var list = new int[] { 1, 2, 3, 4 };
            IQueryable<int> ints = list.AsQueryable();
            IQueryable<int> ints1 = list.AsQueryable().Where(i => i > 2);
            var res = _taxStartegy.Calculate(price);
            return res;

        }
    }

    
}