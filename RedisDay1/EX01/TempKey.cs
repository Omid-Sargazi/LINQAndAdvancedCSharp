using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisDay1.EX01
{
    public class TempKey
    {
        public static async Task Run()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost");
            IDatabase db = redis.GetDatabase();

            await db.StringSetAsync("temp-key", "I Will Expire Soon", TimeSpan.FromSeconds(10));
            Console.WriteLine("Key 'temp-key' set with 10 seconds TTL.");

            Console.WriteLine("Waiting 15 seconds...");
            await Task.Delay(1500);

            var valueAfterTTL = await db.StringGetAsync("temp-key");

            if (valueAfterTTL.IsNullOrEmpty)
            {
                Console.WriteLine("Key 'temp-key' has expired and is no longer available.");
            }
            else
            {
                 Console.WriteLine($"Key 'temp-key' still exists: {valueAfterTTL}");
            }

        }
    }
}