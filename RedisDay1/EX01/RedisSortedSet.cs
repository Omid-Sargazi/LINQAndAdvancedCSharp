using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisDay1.EX01
{
    public class RedisSortedSet
    {
        public static async Task Run()
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost");
            IDatabase db = redis.GetDatabase();
            string leaderboardKey = "game:leaderboard";

            await db.SortedSetAddAsync(leaderboardKey, "Alice", 150);
            await db.SortedSetAddAsync(leaderboardKey, "Bob", 250);
            await db.SortedSetAddAsync(leaderboardKey, "Omid", 550);

            var topPlayers = await db.SortedSetRangeByScoreWithScoresAsync(leaderboardKey, order: Order.Descending);
            Console.WriteLine("Leaderboard:");
            foreach (var player in topPlayers)
                Console.WriteLine($"{player.Element}: {player.Score}");


            long? bobRank = await db.SortedSetRankAsync(leaderboardKey, "Bob", Order.Descending);
            Console.WriteLine($"Bob's Rank: #{bobRank + 1}");

        }
    }
}