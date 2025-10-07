namespace AdventureWorksLINQ.Console.ThreadSafeSingleton
{
    public class Singleton
    {
        private static readonly Singleton _instance = new Singleton();
        private Singleton() { }

        public static Singleton GetInstance()
        {
            return _instance;
        }
    }


    public class LazyCustome<T>
    {
        private  readonly object _lock = new object();
        private T _value;
        private bool _isValueCreated;
        private readonly Func<T> _valueFactory;

        public LazyCustome(Func<T> valueFactory)
        {
            _valueFactory = valueFactory;
        }
        public LazyCustome()
        {
            _valueFactory = () => Activator.CreateInstance<T>();
        }


        public T Value
        {
            get
            {
                if (!_isValueCreated)
                {
                    lock (_lock)
                    {
                        if (!_isValueCreated)
                        {
                            if (_valueFactory != null)
                            {
                                _value = _valueFactory();
                            }
                            else
                            {
                                _value = Activator.CreateInstance<T>();
                            }
                           
                            _isValueCreated = true;
                        }
                    }
                }
                return _value;
            }
        }

    }
     

}