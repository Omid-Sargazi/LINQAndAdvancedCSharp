using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisDay1.EX01

{
    public class UserInfoSystemWithRedis
    {
        public static async Task Run()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost");
            IDatabase db = redis.GetDatabase();

            await db.ListLeftPushAsync("email_queue", "Welcome@site.com");
            await db.ListLeftPushAsync("email_queue", "reset@site.com");
            Console.WriteLine("Emails in queue:");

            var emails = await db.ListRangeAsync("email_queue");

            foreach (var email in emails)
            {
                Console.WriteLine(email);
            }

            Console.WriteLine();

            string userKey = "user:101";
            await db.HashSetAsync(userKey, new HashEntry[]
                { new HashEntry("name", "Omid"),
                new HashEntry("email", "o@o.com"),
                new HashEntry("address", "Poland"),
                new HashEntry("age", 43),
            }
            );

            Console.WriteLine("User Profile:");

            var userData = await db.HashGetAllAsync(userKey);
            foreach (var field in userData)
            {
                Console.WriteLine($"{field.Name}:{field.Value}");
            }

            Console.WriteLine();

            string favKey = "user:101:favorite";
            await db.SetAddAsync(favKey, "CSharp");
            await db.SetAddAsync(favKey, "redis");
            await db.SetAddAsync(favKey, "dotnet");

            var favorite = await db.SetMembersAsync(favKey);
            foreach (var item in favorite)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            string scoreKey = "users_scors";
            await db.SortedSetAddAsync(scoreKey, "Saeed", 250);
            await db.SortedSetAddAsync(scoreKey, "Omid", 350);
            await db.SortedSetAddAsync(scoreKey, "Vahid", 450);

            Console.WriteLine("Leaderboard");
            var leaderboard = await db.SortedSetRangeByScoreWithScoresAsync(scoreKey, order: Order.Ascending);
            foreach (var entry in leaderboard)
            {
                Console.WriteLine($"{entry.Element}:{entry.Score}");
            }

            redis.Dispose();

        }
    }
}