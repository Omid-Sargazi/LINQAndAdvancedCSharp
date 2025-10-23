using System.Reflection;

namespace PluginHost
{
     public class ClientPlugin
    {
        public static void Run()
        {
            var pluginFolder = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
            if (!Directory.Exists(pluginFolder))
            {
                Directory.CreateDirectory(pluginFolder);
            }
            var pluginFiles = Directory.GetFiles(pluginFolder, "*.dll");

            var assemblyPath = pluginFiles[0];
            Assembly pluginAssembly = Assembly.LoadFrom(assemblyPath);

            Console.WriteLine($"✅ Loaded Assembly: {pluginAssembly.FullName}");

            var types = pluginAssembly.GetTypes();
            foreach (var type in types)
            {
                Console.WriteLine($"Found type:{type.FullName}");
            }

            var pluginType = types.FirstOrDefault(t => t.Name == "SmaplePlugin");
            if (pluginType == null)
            {
                Console.WriteLine("❌ Plugin class not found!");
                return;
            }

            object pluginInstance = Activator.CreateInstance(pluginType);
            Console.WriteLine($"Created instance: {pluginType.FullName}");

            var executemethod = pluginType.GetMethod("Execute");
            if (executemethod == null)
            {
                Console.WriteLine("❌ Execute method not found!");
                return;
            }

            executemethod.Invoke(pluginInstance, null);
        }
    }
}