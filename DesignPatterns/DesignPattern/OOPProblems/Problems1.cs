using System.ComponentModel;

namespace DesignPattern.OOPProblems
{
    public interface ICacheSystem
    {
        void Cache(string context);
    }

    public class DistributedCache : ICacheSystem
    {
        public void Cache(string context)
        {
            Console.WriteLine("Cached Data by DistributedCache");
        }
    }

    public class RedisCache : ICacheSystem
    {
        public void Cache(string context)
        {
            Console.WriteLine("Cached Data by RedisCache");

        }
    }

    public class MemoryCache : ICacheSystem
    {
        public void Cache(string context)
        {
            Console.WriteLine("Cached Data by MemoryCache");

        }
    }

    public class CachingSystem
    {
        protected ICacheSystem _cacheSystem;
        public CachingSystem(ICacheSystem cacheSystem)
        {
            _cacheSystem = cacheSystem;
        }
    }

    public class ResourceHungry
    {
        //Class which takes lots of memory if initialized.
    }

    public class ResourceCinsumer
    {
        ResourceHungry _hungry;

        private readonly object _resourceHungryLock = new object();
        public ResourceHungry Hungey()
        {
            lock(_resourceHungryLock)
            {
                if (_hungry == null)
            {
                _hungry = new ResourceHungry();
            }
                return _hungry;
            }
            

        }
    }


}