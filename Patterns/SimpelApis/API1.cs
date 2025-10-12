using System.Text.Json;

namespace Patterns.SimpelApis
{
    public class API1
    {
        public async static  Task<string> Run()
        {
            try
            {
                using var httpClient = new HttpClient();
                var result = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/users/1");
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
        public async static Task<string> Run()
        {
            try
            {
                using var httpClient = new HttpClient();
                var result = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/users/2");
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
        public async static Task<string[]> Run()
        {
            var task1 = API1.Run();
            var task2 = API2.Run();


            var result = new List<string>();
            try
            {
                var str = await Task.WhenAll(task1, task2);
                var user1 = JsonSerializer.Deserialize<ApiObject>(task1.Result);
                var user2 = JsonSerializer.Deserialize<ApiObject>(task2.Result);

                Console.WriteLine(user1.name);
                Console.WriteLine(user2.name);

                return str;
            }
            catch (System.Exception)
            {

                if (task1.IsCompletedSuccessfully)
                {
                    result.Add(task1.Result);
                    Console.WriteLine("task 1 is not completed.");
                } else
                {
                    result.Add("API1 Is Failed");
                }
                if (task2.IsCompletedSuccessfully)
                {
                    result.Add(task2.Result);
                    Console.WriteLine("task 2 is not completed.");
                }
                else
                {
                    result.Add("Api2 Is Faied");
                }

            }
            return result.ToArray();
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