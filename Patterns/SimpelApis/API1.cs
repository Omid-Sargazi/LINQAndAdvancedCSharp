using System.Text.Json;

namespace Patterns.SimpelApis
{
    public class API1
    {
        private readonly HttpClient _httpClient;
        public API1(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> Run()
        {
            try
            {
                
                var result = await _httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/users/1");
                return result;
            }
            catch (HttpRequestException ex)
            {

                throw new Exception("Error in API1");
            }
            
        }
    }

    public class API2
    {
        private readonly HttpClient _httpClient;
        public API2(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> Run()
        {
            try
            {
              
                var result = await _httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/users/2");
                 return result;
            }
            catch (HttpRequestException ex)
            {

                throw new Exception("Error in API2");

            }
           
        }

    }

    public class ExecuteTwoSyncMethod
    {
        private readonly API1 _api1;
        private readonly API2 _api2;
        public ExecuteTwoSyncMethod(API1 api1, API2 api2)
        {
            _api1 = api1;
            _api2 = api2;
        }
        public async Task<string> Run()
        {
           
             var t1 =_api1.Run();
            var t2 = _api2.Run();


            var result = new List<string>();
            try
            {
                var str = await Task.WhenAll(t1, t2);
                var user1 = JsonSerializer.Deserialize<ApiObject>(t1.Result);
                var user2 = JsonSerializer.Deserialize<ApiObject>(t2.Result);

                Console.WriteLine(user1.name);
                Console.WriteLine(user2.name);

                var usesr = new List<ApiObject> { user1, user2 };
                var merge = JsonSerializer.Serialize(usesr);

                return merge;
            }
            catch (System.Exception)
            {

                if (t1.IsCompletedSuccessfully)
                {
                    result.Add(t1.Result);
                    Console.WriteLine("task 1 is not completed.");
                } else
                {
                    result.Add("API1 Is Failed");
                }
                if (t2.IsCompletedSuccessfully)
                {
                    result.Add(t2.Result);
                    Console.WriteLine("task 2 is not completed.");
                }
                else
                {
                    result.Add("Api2 Is Faied");
                }

            }
            return "";
        }
    }

    public class ApiObject
    {
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public Address address { get; set; }
       
    }

    public class Address
    {
        public string street { get; set; }
        public string city { get; set; }
    }

}