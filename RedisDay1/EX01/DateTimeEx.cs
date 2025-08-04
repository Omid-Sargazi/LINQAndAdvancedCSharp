using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisDay1.EX01
{
    public class DateTimeEx
    {
        public static async Task Run()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost");
            IDatabase db = redis.GetDatabase();

            string todayDate = DateTime.UtcNow.ToString("yyyy-MM-dd");

            await db.StringSetAsync("today", todayDate);

            string storedDate = await db.StringGetAsync("today");
            Console.WriteLine($"Today`s date stored in Redis:{storedDate}");
        }
    }
}