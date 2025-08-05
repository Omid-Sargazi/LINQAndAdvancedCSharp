using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisDay1.EX01
{
    public class HashSetAsyncRedis
    {
        public static async Task Run()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost");
            IDatabase db = redis.GetDatabase();

            
            string userKey = "user:1001";

            await db.HashSetAsync(userKey, new HashEntry[]

               {
                new HashEntry("name", "Omid"),
                new HashEntry("email","o@o.com"),
                new HashEntry("age",42)

                }
            );

            string name = await db.HashGetAsync(userKey, "name");
            Console.WriteLine($"User name: {name}");

            var allFields = await db.HashGetAllAsync(userKey);
            Console.WriteLine("User info:");
            foreach (var entry in allFields)
                Console.WriteLine($"{entry.Name}: {entry.Value}");

        }
    }
}