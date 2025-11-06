using System.ComponentModel;

namespace DesignPattern.OOPProblems
{

    public enum CacheStrategyType
    {
        Memory,
        Redis,
        Distributed,
    }
    public interface ICacheSystem
    {
        void Cache(string context);
    }

    public class DistributedCache : ICacheSystem
    {
        public void Cache(string context)
        {
            Console.WriteLine($"Cached Data by DistributedCache  {context}");
        }
    }

    public class RedisCache : ICacheSystem
    {
        public void Cache(string context)
        {
            Console.WriteLine($"Cached Data by RedisCache  {context}");

        }
    }

    public class MemoryCache : ICacheSystem
    {
        public void Cache(string context)
        {
            Console.WriteLine($"Cached Data by MemoryCache  {context}");

        }
    }

    public class CachingSystem
    {
        private readonly Func<ICacheSystem> _factory;
        private readonly Lazy<ICacheSystem> _lazyCache;
        // protected ICacheSystem _cacheSystem;
        public CachingSystem(Func<ICacheSystem> factory)
        {
            _factory = factory;
            _lazyCache = new Lazy<ICacheSystem>(() => _factory());
        }

        public void Cache(string context)
        {
            _lazyCache.Value.Cache(context);
        }
    }


    public class CacheClient
    {
        public static void Execute()
        {
            var caching = new CachingSystem(() => new RedisCache());
            caching.Cache("data");
            caching.Cache("data22");
        }
    }


}