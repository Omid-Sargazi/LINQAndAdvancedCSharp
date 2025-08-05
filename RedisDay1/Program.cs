using System.Threading.Tasks;
using RedisDay1.EX01;
using StackExchange.Redis;
public class Program
{
    public static async Task Main(string[] args)
    {
        // System.Console.WriteLine("Hello Redis");
        // var redis = await ConnectionMultiplexer.ConnectAsync("localhost");
        // IDatabase db = redis.GetDatabase();

        // await db.StringSetAsync("name", "Farhad");

        // string value = await db.StringGetAsync("name");
        // Console.WriteLine($"Value of 'name': {value}");

        // // بررسی وجود کلید
        // bool exists = await db.KeyExistsAsync("name");
        // Console.WriteLine($"Exists? {exists}");

        // // حذف کلید
        // await db.KeyDeleteAsync("name");

        // redis.Dispose();

        // await Excersice.Run();
        // await DateTimeEx.Run();

        // await TempKey.Run();
        // await ListRedis.Run();
        // await HashSetAsyncRedis.Run();
        await RedisSet.Run();
    }
}