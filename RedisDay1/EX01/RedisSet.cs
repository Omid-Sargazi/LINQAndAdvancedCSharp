using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisDay1.EX01
{
    public class RedisSet
    {
        public static async Task Run()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost");
            IDatabase db = redis.GetDatabase();

            string tagsKey = "tags:article:123";

            await db.SetAddAsync(tagsKey, "redis");
            await db.SetAddAsync(tagsKey, "nosql");
            await db.SetAddAsync(tagsKey, "caching");
            await db.SetAddAsync(tagsKey, "redis");

            var tags = await db.SetMembersAsync(tagsKey);
            Console.WriteLine("Tags:");
            foreach (var item in tags)
            {
                Console.WriteLine(item);
            }

            bool isRedisTag = await db.SetContainsAsync(tagsKey, "redis");
            Console.WriteLine($"Is 'redis' a tag? {isRedisTag}");
        }
    }
}