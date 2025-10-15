using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Patterns.TaskProblem
{
    public enum TaskPriority

    {
        Low = 1,
        Normal = 2,
        High = 3,
        Critical=4,
    };

    public interface ITask
    {
        TaskPriority Priority { get; }
        Task ExecuteAsync();
    }

    public class Tasks : ITask
    {
        public TaskPriority Priority { get; }

        public Tasks(TaskPriority taskPriority)
        {
            Priority = taskPriority;
        }

        public Task ExecuteAsync()
        {
            Console.WriteLine($"Running task with priority: {Priority}");
            return Task.CompletedTask;
        }
    }

    public class TaskQueue
    {
        private List<ITask> _tasks = new List<ITask>();

        public TaskQueue() { }

        public void AddTask(ITask task)
        {
            _tasks.Add(task);
        }

        public void RemoveTask(ITask task)
        {
            var res = _tasks.Remove(task);

        }

        public async Task RunAllAsync()
        {
            foreach (var task in _tasks)
            {
                await task.ExecuteAsync();
            }
        }

        public async Task RunOnPriorityAsync()
        {
            var resCritical = _tasks.FirstOrDefault(t => t.Priority == TaskPriority.Critical);
            var res = _tasks.OrderByDescending(t => t.Priority);
            
            foreach(var task in res)
            {
                await task.ExecuteAsync();
            }
        }
    }

    public class ClientTask
    {
        public static async Task Run()
        {
            TaskQueue task = new TaskQueue();
            task.AddTask(new Tasks(TaskPriority.High));
            task.AddTask(new Tasks(TaskPriority.Critical));
            task.AddTask(new Tasks(TaskPriority.Normal));
            task.AddTask(new Tasks(TaskPriority.Critical));
            task.AddTask(new Tasks(TaskPriority.High));

            // await task.RunAllAsync();
            await task.RunOnPriorityAsync();
        }
    }
}