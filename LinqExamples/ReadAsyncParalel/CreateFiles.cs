using System.Text;
using System.Threading.Tasks;

namespace LinqExamples.ReadAsyncParalel
{
    public class CreateFiles
    {
        public void WriteFile()
        {
            string directoryPath = "Files";
            Directory.CreateDirectory(directoryPath);

            try
            {
                for (int i = 0; i < 500; i++)
                {
                    string path = Path.Combine(directoryPath, $"file{i}.txt");
                    using (FileStream fs = File.Create(path))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes($"This is some text in the file{i}.");
                        fs.Write(info, 0, info.Length);
                    }

                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }

    public class ReadAsyncFile
    {
        private readonly string _filePath;
        private readonly string _fileName;

        public ReadAsyncFile(string filePath, string fileName)
        {
            _fileName = fileName;
            _filePath = filePath;
        }
        public async Task<string> Execute()
        {
            string filePath = Path.Combine(_filePath, _fileName);

            return await File.ReadAllTextAsync(filePath);
        }
    }

    public class ClientParallelAsync
    {
        private readonly List<Task<string>> _tasks = new List<Task<string>>();
        public async Task Run()
        {
            string directoryPath = "Files";

            for (int i = 0; i < 500; i++)
            {
                var res = new ReadAsyncFile(directoryPath, $"file{i}.txt");
                _tasks.Add(res.Execute());
            }

            string[] results = await Task.WhenAll(_tasks);
            Console.WriteLine($"{string.Join(",",results)}");
        }
    }
}