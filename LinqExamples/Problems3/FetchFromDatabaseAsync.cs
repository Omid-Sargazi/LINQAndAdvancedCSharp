namespace LinqExamples.Problems3
{
    public class Senario1
    {
        private async Task<int> CalculateAsync(int resquestId)
        {
            await Task.Delay(2000);
            return resquestId * 2;
        }

        public async void UseTask()
        {
            int res = await CalculateAsync(5);
            Console.WriteLine(res);
        }

        public async ValueTask<int> CalculateFastAsync(int number)
        {
            if (number < 10)
            {
                return number * 2;
            }
            await Task.Delay(100);
            return number * 2;
        }

        public async Task<int> ProcessRequestTask(int request)
        {
            return request * 2;
        }
        
        public ValueTask<int> ProcessRequestValueTask(int reqId)
        {
            return new ValueTask<int>(reqId * 2);
        }
    }

    public class s
    {
        public string Name { get; set; }
    }
    
    public struct Point
    {
        public int X, Y;
    }
}