namespace ThreadingProblems.Problems
{
    public class SyncConnection
    {
        public static void Execute()
        {
            var httpClient = new HttpClient();
            Console.WriteLine("شروع دانلود...");

            // ❌ متد بلاک‌کننده
            var content = httpClient.GetStringAsync("https://api.github.com/").Result;

            Console.WriteLine("دانلود تمام شد!");
            Console.WriteLine($"طول محتوا: {content.Length}");
        }
        
        public async static Task Execute2()
        {
            var httpClient = new HttpClient();
             httpClient.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");
        Console.WriteLine("شروع دانلود...");

        // ✅ non-blocking
       try
            {
                // ✅ غیر بلاک‌کننده و درست
                var content = await httpClient.GetStringAsync("https://api.github.com/");
                Console.WriteLine("دانلود تمام شد!");
                Console.WriteLine($"طول محتوا: {content.Length}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطا: {ex.Message}");
            }
        }
    }
}