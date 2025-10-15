using System.IO.Pipelines;
using System.Security.Cryptography.X509Certificates;

namespace DesignPattern.TaksProblem
{
    public enum TaskPriority
    {
        Low = 0,
        Normal = 1,
        High = 2,
        Critical = 3,

    };

    public interface ITasks
    {
        TaskPriority Priority{ get; }
        Task ExecuteAsync();
    }

    public class Task1 : ITasks
    {
        public TaskPriority Priority { get; } = TaskPriority.Critical;

        public async Task ExecuteAsync()
        {
            await Task.FromResult("task1 run");
        }
    }

    public class Task2 : ITasks
    {
        public TaskPriority Priority { get; } = TaskPriority.Normal;

        public async Task ExecuteAsync()
        {
            await Task.FromResult("task1 run");

        }
    }

    public class TaskQueue
    {
        private List<ITasks> _tasks = new List<ITasks>();
        
        public TaskQueue()
        {
            
        }

        public void Enqueue(ITasks task)
        {
            _tasks.Add(task);
        }

        public ITasks Dequeue()
        {
            if (_tasks.Count == 0)
                return null;
            var highestPriorityTask = _tasks.OrderByDescending(t => t.Priority).First();

            _tasks.Remove(highestPriorityTask);
            return highestPriorityTask;
        }
        
        public ITasks Peek()
        {
            if (_tasks.Count == 0)
                return null;

            return _tasks.OrderByDescending(t => t.Priority).First();
        }
    }
}