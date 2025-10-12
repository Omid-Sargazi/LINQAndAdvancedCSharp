using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Patterns.CompineAPIProject
{
    public class FirstApi
    {
        private readonly HttpClient _httpClient;
        public FirstApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> Run()
        {
            return await _httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts/1");
        }
    }

    public class SecondApi
    {
        private readonly HttpClient _httpClient;
        public SecondApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> Run()
        {
            return await _httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts/2");
        }
    }

    public class CombineTwoApi
    {
        private readonly FirstApi _firstApi;
        private readonly SecondApi _secondApi;
        public CombineTwoApi(FirstApi firstApi, SecondApi secondApi)
        {
            _firstApi = firstApi;
            _secondApi = secondApi;
        }

        public async Task<string> Run()
        {
            var task1 = _firstApi.Run();
            var task2 = _secondApi.Run();
            await Task.WhenAll(task1, task2);

            var result1 = task1.Result;
            var result2 = task2.Result;

            var post1 = JsonSerializer.Deserialize<PostData>(await task1);
            var post2 = JsonSerializer.Deserialize<PostData>(await task2);

            var combineResult = new
            {
                Posts = new[] { post1, post2 },
                TotalCount = 2
            };
            return JsonSerializer.Serialize(combineResult);
            
        }
    }

    public class PostData  // به جای SalesData
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }


    public class InventoryData
    {
        public int TotalItems { get; set; }
        public int LowStockItems { get; set; }
    }
}