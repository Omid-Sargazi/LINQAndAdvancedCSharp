using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DesignPattern.OOPProblems
{

    public enum CacheStrategyType
    {
        Memory,
        Redis,
        Distributed,
    }
    public interface ICacheSystem
    {
        void Cache(string context);
    }

    public class DistributedCache : ICacheSystem
    {
        public void Cache(string context)
        {
            Console.WriteLine($"Cached Data by DistributedCache  {context}");
        }
    }

    public class RedisCache : ICacheSystem
    {
        public void Cache(string context)
        {
            Console.WriteLine($"Cached Data by RedisCache  {context}");

        }
    }

    public class MemoryCache : ICacheSystem
    {
        public void Cache(string context)
        {
            Console.WriteLine($"Cached Data by MemoryCache  {context}");

        }
    }

    public class CachingSystem
    {
        private readonly Func<ICacheSystem> _factory;
        private readonly Lazy<ICacheSystem> _lazyCache;
        // protected ICacheSystem _cacheSystem;
        public CachingSystem(Func<ICacheSystem> factory)
        {
            _factory = factory;
            _lazyCache = new Lazy<ICacheSystem>(() => _factory());
        }

        public void Cache(string context)
        {
            _lazyCache.Value.Cache(context);
        }
    }

    public class CacheStrategyFactory
    {
        private readonly Dictionary<CacheStrategyType, Lazy<ICacheSystem>> _strategies;

        public CacheStrategyFactory()
        {
            _strategies = new Dictionary<CacheStrategyType, Lazy<ICacheSystem>>
            {
                [CacheStrategyType.Redis] = new Lazy<ICacheSystem>(() => new RedisCache()),
                [CacheStrategyType.Memory] = new Lazy<ICacheSystem>(() => new MemoryCache()),
                [CacheStrategyType.Distributed] = new Lazy<ICacheSystem>(() => new DistributedCache()),
            };
        }
        
        public ICacheSystem GetStrategy(CacheStrategyType type)
        {
            return _strategies[type].Value;
        }
    }


    public class CacheClient
    {
        public static void Execute()
        {
            var factory = new CacheStrategyFactory();

            var cache = new CachingSystem(() => factory.GetStrategy(CacheStrategyType.Redis));
            cache.Cache("data");

            var memoryCache = new CachingSystem(() => new MemoryCache());
            memoryCache.Cache("data2");
        }
    }


    public interface IValidationRule<T>
    {
        string Validate(T obj);
    }

    public class EmailRequiredRule : IValidationRule<User>
    {
        public string Validate(User obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Email))
            {
                return "Email Is Required";
            }

            return null;
        }
    }

    public class EmailMustContainAtRule : IValidationRule<User>
    {
        public string Validate(User obj)
        {
            if (!obj.Email.Contains("@"))
                return "Email must be have @";
            return null;
        }
    }

    public class AgeRangeRule : IValidationRule<User>
    {
        public string Validate(User obj)
        {
            if (obj.Age < 0 || obj.Age > 150)
                return "Age must be between 0 and 150";

            return null;
        }
    }

    public class Validator<T>
    {
        private List<IValidationRule<T>> _rules = new List<IValidationRule<T>>();

        public void AddRule(IValidationRule<T> rule)
        {
            _rules.Add(rule);
        }

        public List<string> Validate(T obj)
        {
            List<string> errors = new List<string>();

            foreach (var rule in _rules)
            {
                var result = rule.Validate(obj);
                if (result != null)
                {
                    errors.Add(result);
                }
            }

            return errors;
        }
    }

    public class User
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }


    public interface INotificationObserver
    {
        void Update(string message);
    }

    public class EmailObserver : INotificationObserver
    {
        private string _email;

        public EmailObserver(string email)
        {
            _email = email;
        }
        public void Update(string message)
        {
            Console.WriteLine($"Email to{_email}:{message}");
        }
    }

    public class SMSObserver : INotificationObserver
    {
        private readonly string _sms;
        public SMSObserver(string sms)
        {
            _sms = sms;
        }
        public void Update(string message)
        {
            Console.WriteLine($"SMS to{_sms}:{message}");

        }
    }

    public class PushObserver : INotificationObserver
    {
        private readonly string _deviceid;
        public PushObserver(string deviceId)
        {
            _deviceid = deviceId;
        }
        public void Update(string message)
        {
            Console.WriteLine($"Push to{_deviceid}:{message}");
        }
    }

    public class NotificationSubject
    {
        protected readonly List<INotificationObserver> _observer = new List<INotificationObserver>();
        private readonly object _lock = new object();

        public void Subscribe(INotificationObserver observer)
        {
            if (observer == null) return;
            lock (_lock)
            {
                if (!_observer.Contains(observer))
                {
                    _observer.Add(observer);
                }
            }
        }

        public void Unsubscribe(INotificationObserver observer)
        {
            if (observer == null)
            {
                return;
            }
            lock (_lock)
            {
                _observer.Remove(observer);
            }
        }

        public void NotifyAll(string message)
        {
            List<INotificationObserver> snapshot;
            lock (_lock)
            {
                snapshot = new List<INotificationObserver>(_observer);
            }

            foreach (var obj in snapshot)
            {
                try
                {
                    obj.Update(message);
                }
                catch (System.Exception ex)
                {

                    Console.Error.WriteLine($"Observer error: {ex.Message}");
                }
            }
        }
    }
    

    public class LinqExamples
    {
        public static void Execute()
        {
            var products = new[]
            {
                new { Name = "Laptop", Category = "Electronics", Price = 1000 },
                new { Name = "Mouse", Category = "Electronics", Price = 50 },
                new { Name = "Keyboard", Category = "Electronics", Price = 120 },
                new { Name = "Monitor", Category = "Electronics", Price = 800 },
                new { Name = "Shirt", Category = "Clothing", Price = 30 },
                new { Name = "Pants", Category = "Clothing", Price = 45 },
                new { Name = "Shoes", Category = "Clothing", Price = 80 },
                new { Name = "Jacket", Category = "Clothing", Price = 120 },
                new { Name = "Phone", Category = "Electronics", Price = 900 }
            };

            var result1 = products.GroupBy(p => p.Category)
            .Select(g => new { Category = g.Key, TopPrices = g.OrderByDescending(g => g.Price).Take(2).Select(p => $"{p.Name}{p.Price}") }).OrderBy(x => x.Category);


            int[] numbers = { 12, 34, 56, 78, 91, 23, 45, 67, 89, 100, 111, 222, 333 };

            var res2 = numbers.Where(SumOfDigits);
            // Console.WriteLine($"{string.Join(",", res2)}");


            var students = new[]
            {
                new { Name = "Ali", BirthDate = new DateTime(2000, 11, 15) },
                new { Name = "Sara", BirthDate = new DateTime(1999, 10, 8) },
                new { Name = "Reza", BirthDate = new DateTime(2001, 9, 20) },
                new { Name = "Fatemeh", BirthDate = new DateTime(1998, 1, 25) },
                new { Name = "Mohammad", BirthDate = new DateTime(2002, 3, 10) },
                new { Name = "Zahra", BirthDate = new DateTime(2000, 1, 5) }
            };

            var res3 = students.Where(s => s.BirthDate.Month == DateTime.Now.Month).Select(s => new { Name = s.Name, Age = (DateTime.Now.Year - s.BirthDate.Year) -
            (DateTime.Now.DayOfYear < s.BirthDate.DayOfYear ? 1 : 0) });
            foreach (var student in res3)
            {
                Console.WriteLine($"{student.Name}: {student.Age} years");
            }

            string[] words = { "spoon", "hello", "wolf", "fed", "cba", "onion", "red", "abcd" };
            var res4 = words.Where(IsReverseAlphabetical).OrderByDescending(s => s);


            int[] numbers2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

            var res7 = numbers2.Where(n => n % 3 == 0 || n % 5 == 0).OrderBy(n => n);

            var products2 = new[]
            {
                new { Name = "Laptop", Category = "Electronics", Price = 1000, Rating = 4.5 },
                new { Name = "Mouse", Category = "Electronics", Price = 50, Rating = 4.2 },
                new { Name = "Keyboard", Category = "Electronics", Price = 120, Rating = 4.7 },
                new { Name = "Shirt", Category = "Clothing", Price = 30, Rating = 4.0 },
                new { Name = "Pants", Category = "Clothing", Price = 45, Rating = 4.3 },
                new { Name = "Shoes", Category = "Clothing", Price = 80, Rating = 4.8 },
                new { Name = "Phone", Category = "Electronics", Price = 900, Rating = 4.6 }
            };
            var res8 = products2.GroupBy(p => p.Category).
            Select(g => new { Name = g.Key, HighRating = g.OrderByDescending(p => p.Rating).FirstOrDefault() }).OrderBy(p => p.Name);

            Console.WriteLine($"{string.Join(",", res8)}");

            int[] numbers3 = { 12, 23, 34, 45, 56, 67, 78, 89, 91, 100, 111, 123, 456, 789 };

            var res9 = numbers.Where(SumOfDigits2).OrderBy(p => p);
            Console.WriteLine($"{string.Join(",", res9)}");

            string[] sentences =
            {
                "The quick brown fox",
                "jumps over the lazy dog",
                "Hello world from C#",
                "LINQ is powerful tool"
            };

            var commonLetters = sentences
            .Select(s => s.ToLower().Where(char.IsLetter).Distinct())
            .Aggregate((cur, next) => cur.Intersect(next)).OrderBy(p => p);

            int[] nums = { 1, 2, 3, 4, 5 };
            int sum = nums.Aggregate((cur, next) => cur + next);
            int pro = nums.Aggregate((cur, next) => cur * next);
            int sum2 = nums.Aggregate(10, (cur, next) => cur + next);

            string[] wordss = { "Hello", "World", "LINQ" };
            string result = wordss.Aggregate((cur, next) => cur + " " + next);

            int max = nums.Aggregate((cur, next) => cur > next ? cur : next);
            string[] words2 = { "I", "Love", "C#" };
            var res11 = words2.Aggregate((cur, next) => cur + " " + next);
            int n = 5;
            var num12 = Enumerable.Range(1, 5);
            var factorial = num12.Aggregate((cur, next) => cur * next);
                        
                        
        }

        public static bool SumOfDigits(int num)
        {
            int sum = 0;
            while (num >= 1)
            {
                sum += num % 10;
                num /= 10;
            }
            if (sum % 2 != 0)
            {
                return true;
            }
            return false;
        }

        public static bool SumOfDigits2(int num)
        {
            int sum = 0;
            while (num >= 1)
            {
                sum += num % 10;
                num /= 10;
            }

            int prime = 0;

            for (int i = 1; i <= sum; i++)
            {
                if (sum % i == 0)
                {
                    prime++;
                }
            }

            if (prime == 2 || prime==1)
            {
                return true;
            }
            return false;
        }

        private static bool IsReverseAlphabetical(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] > s[i - 1])
                {
                    return true;
                }
            }

            return false;
        }


    }

}