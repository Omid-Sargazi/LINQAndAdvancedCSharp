namespace DIPattern.Loggers
{
    public interface IRepository
    {
        Task GetFromCache(string context);
        Task GetFromDatabase(string context);
    }
    public class RepositoryCache : IRepository
    {
        private List<string> cache = new List<string>();

        public async Task GetFromCache(string context)
        {
            if (!cache.Contains(context))
            {
                GetFromDatabase(context);
                await Task.Delay(1000);
            }

            Console.WriteLine($"Get Data From Ca{context}");
        }

        public async Task GetFromDatabase(string content)
        {
            await Task.Delay(1000);
            cache.Add(content);
            Console.WriteLine($"Cache From Database{content}");
        }
    }
}