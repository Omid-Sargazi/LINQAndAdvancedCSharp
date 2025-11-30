using StackExchange.Redis;

namespace RedisDocExamples.Examples
{
    public class ConnectBasicExample
    {
        public void run()
    {
        // ۱. اتصال به دیتابیس Redis Cloud شما
        var muxer = ConnectionMultiplexer.Connect(
            new ConfigurationOptions
            {
                EndPoints = { {"redis-18568.c244.us-east-1-2.ec2.cloud.redislabs.com", 18568} },
                User = "default",
                Password = "VtZJO9kHDyLD3gYMnv07OYicjbLWax9a"
            }
        );

        // ۲. گرفتن یک دیتابیس (معمولاً شماره 0)
        var db = muxer.GetDatabase();

        // ۳. ذخیره یک مقدار ساده (مثل key-value)
        db.StringSet("foo", "bar");

        // ۴. خوندن همون مقدار
        RedisValue result = db.StringGet("foo");

        // ۵. چاپ روی کنسول → خروجی: bar
        Console.WriteLine(result); // >>> bar
    }
    }
}