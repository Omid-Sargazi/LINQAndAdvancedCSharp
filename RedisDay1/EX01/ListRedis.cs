using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisDay1.EX01
{
    public class ListRedis
    {
        public static async Task Run()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost");
            IDatabase db = redis.GetDatabase();

            await db.ListLeftPushAsync("message-queue", "Message1");
            await db.ListLeftPushAsync("message-queue", "Message2");
            await db.ListLeftPushAsync("message-queue", "Message3");

            var messgaes = await db.ListRangeAsync("message-queue");
            Console.WriteLine("All messages in queue:");
            foreach (var item in messgaes)
            {
                Console.WriteLine(item);
            }

            var popped = await db.ListRightPopAsync("message-queue");
            Console.WriteLine($"Popped message: {popped}");
        }
    }
}