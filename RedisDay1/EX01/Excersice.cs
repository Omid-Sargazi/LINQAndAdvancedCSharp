using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisDay1.EX01
{
    public class Excersice
    {
        public static async Task Run()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost");
            IDatabase db = redis.GetDatabase();

            await db.StringSetAsync("counter", 10);
            for (int i = 0; i <= 5; i++)
            {
                long newValue = await db.StringIncrementAsync("counter");
                Console.WriteLine($"New counter value after increment{i + 1}:{newValue}");
            }

            Console.WriteLine($"Final counter value:"+await db.StringGetAsync("counter"));
        }
    }
}