using System.Data.Common;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisRelationships.UserRealtionship
{
    public class RedisRelations
    {
        public static async Task Run()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost");
            var db = redis.GetDatabase();

            var userId = "42";
            await db.HashSetAsync($"user:{userId}", new HashEntry[]
            {
                new HashEntry("name", "Omid"),
                new HashEntry("email","o@o.com"),
            });


            string postId = "1001";
            await db.HashSetAsync($"post:{postId}", new HashEntry[]
            {
                new HashEntry("title","Learning Redis"),
                new HashEntry("content","Today I modeled relations in Redis")
            });

            await db.SetAddAsync($"user:{userId}:posts", postId);

            await db.SetAddAsync($"post:{postId}:likes", userId);
            await db.SetAddAsync($"user:{userId}:likes", postId);

            await db.SetAddAsync($"user:{userId}:follows", "55");
            await db.SetAddAsync($"user:55:followers", userId);


            var posts = await db.SetMembersAsync($"user:{userId}:posts");
            var likes = await db.SetMembersAsync($"user:{userId}:likes");
            var followers = await db.SetMembersAsync($"user:{userId}:followers");

                Console.WriteLine("✅ پست‌های Farhad:");
            foreach (var id in posts)
                Console.WriteLine($"- Post ID: {id}");

            Console.WriteLine("\n✅ لایک‌های Farhad:");
            foreach (var id in likes)
                Console.WriteLine($"- Post ID: {id}");

            Console.WriteLine("\n✅ Farhad دنبال می‌کند:");
            foreach (var id in followers)
                Console.WriteLine($"- User ID: {id}");

            redis.Dispose();
        }
    }
}