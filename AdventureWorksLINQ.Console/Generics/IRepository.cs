namespace AdventureWorksLINQ.Console.Generics
{
    public interface IRepository<T>
    {
        T GetById(int id);
    }

    public class Repository<T> : IRepository<T>
    {
        public T GetById(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class DependencyInjection
    {
        public static void Run()
        {
            int? age = null;

            if (age.HasValue)
            {
                System.Console.WriteLine($"Age is : {age.Value}");
            }
            else
            {
                System.Console.WriteLine("Age is unknown");
            }

            System.Console.WriteLine(age?.ToString() ?? "Unknown");
        }
    }
}