namespace AdventureWorksLINQ.Console.DependancyInjection
{
    public interface IOrder1
    {
        public void A();
        public void B();
    }

    public class Order1 : IOrder1
    {
        public void A()
        {
            System.Console.WriteLine("I'm A class");
        }

        public void B()
        {
            System.Console.WriteLine("I'm B class");

        }
    }

    public interface IOrder2
    {
        public void C();
        public void D();
    }

    public class Order2 : IOrder2
    {
        public void C()
        {
            System.Console.WriteLine("I'm C class");

        }

        public void D()
        {
            System.Console.WriteLine("I'm D class");

        }
    }


    public class Master
    {
        private readonly IOrder1 _order1;
        private readonly IOrder2 _order2;

        public Master(IOrder1 order1, IOrder2 order2)
        {
            _order1 = order1;
            _order2 = order2;
        }

        public void DoWork()
        {
            _order1.A();
            _order1.B();
            _order2.C();
            _order2.D();
        }
    }
}

 

