namespace DIPattern.Loggers
{
    public interface ILogger
    {
        void Log(string context);
    }
    public class DbLogger : ILogger
    {
        public void Log(string context)
        {
            throw new NotImplementedException();
        }
    }

    public class FileLogger : ILogger
    {
        public void Log(string context)
        {
            throw new NotImplementedException();
        }
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string context)
        {
            throw new NotImplementedException();
        }
    }
}