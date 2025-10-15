using System.ComponentModel;
using System.Formats.Asn1;
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
        public bool IsRunning { get; }
        public bool IsCompleted { get; }
        Task ExecuteAsync();
    }

    public class Tasks : ITask
    {
        public TaskPriority Priority { get; }
        public string Name { get; }

        public bool IsRunning { get;}

        public bool IsCompleted { get; }

        public Tasks(string name,TaskPriority taskPriority)
        {
            Priority = taskPriority;
            Name = name;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine($"[START] {Priority}");
            Console.WriteLine($"Running task with priority: {Priority}");
            await Task.Delay(1000);
            Console.WriteLine($"[END] {Priority}");
            // return Task.CompletedTask;
        }
        
        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"[START] {Name} ({Priority}) on Thread {Environment.CurrentManagedThreadId}");

            await Task.Delay(700 + (int)Priority * 200, cancellationToken);

            try
            {
                  if (Priority == TaskPriority.Low && new Random().Next(0, 4) == 0)
            {
                throw new InvalidOperationException($"Simulated error in {Name}");
            }
            Console.WriteLine($"[END]   {Name} ({Priority})");
            }
            catch (OperationCanceledException)
            {
                
                 Console.WriteLine($"[CANCELED] {Name}");
                throw;
            }

        }
    }

    public class TaskQueue
    {
        private List<ITask> _tasks = new List<ITask>();

        public async Task RunWithConcurrencyLimitAsync(int maxConcurrency, CancellationToken cancellationToken=default)
        {
            if (maxConcurrency <= 0) throw new ArgumentOutOfRangeException(nameof(maxConcurrency));

            var sem = new SemaphoreSlim(maxConcurrency);
            var ordered = _tasks.OrderByDescending(t => t.Priority).ToList();

            var runningTasks = new List<Task>();

            foreach(var itask in ordered)
            {
                await sem.WaitAsync(cancellationToken);

                var wrapper = Task.Run(async () =>
                {
                    try
                    {
                        await itask.ExecuteAsync();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"[EX] {ex.Message}");
                        throw;
                    }
                    finally

                    {
                        sem.Release();
                    }


                }, cancellationToken);

                runningTasks.Add(wrapper);

                try
                {
                    await Task.WhenAll(runningTasks);
                }
                catch (System.Exception)
                {

                    throw;
                }
                finally

                                {
                    _tasks.Clear();
                }
            }
        }

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

            foreach (var task in res)
            {
                await task.ExecuteAsync();
            }
        }
        
        public async Task RunInBatchesAsync()
        {
            var sortedTask = _tasks.OrderByDescending(t => t.Priority).ToList();
            for (int i = 0; i < sortedTask.Count; i += 3)
            {
                var res = sortedTask.Skip(i).Take(3);
                await Task.WhenAll(res.Select(t => t.ExecuteAsync()));
            }
            _tasks.Clear();
        }
    }

    public class ClientTask
    {
        public static async Task Run()
        {
            // TaskQueue task = new TaskQueue();
            // task.AddTask(new Tasks("T1",TaskPriority.High));
            // task.AddTask(new Tasks(TaskPriority.Critical));
            // task.AddTask(new Tasks(TaskPriority.Normal));
            // task.AddTask(new Tasks(TaskPriority.Critical));
            // task.AddTask(new Tasks(TaskPriority.High));

            // await task.RunAllAsync();
            // await task.RunOnPriorityAsync();

            // await task.RunInBatchesAsync();

            var q = new TaskQueue();
            q.AddTask(new Tasks("T1", TaskPriority.High));
            q.AddTask(new Tasks("T2", TaskPriority.Critical));
            q.AddTask(new Tasks("T3", TaskPriority.Normal));
            q.AddTask(new Tasks("T4", TaskPriority.Critical));
            q.AddTask(new Tasks("T5", TaskPriority.High));
            q.AddTask(new Tasks("T6", TaskPriority.Low));

            var cts = new CancellationTokenSource();

            await q.RunWithConcurrencyLimitAsync(3, cts.Token);
            Console.WriteLine("All DOne");
        }
    }
}