using System.Reflection;

namespace ReflectionProblems.Problem1
{
    public interface IPlugin
    {
        string Name { get; }
        void Execute();
    }

    public class PluginExecute
    {
        string pluginFolder = "MyPlugins";

        public void Run()
        {
            var pluginAssemblies = Directory.GetFiles(pluginFolder, "*.dll").Select(file => Assembly.LoadFrom(file)).ToList();

            Console.WriteLine($"{string.Join(",", pluginAssemblies)}");

            var pluginTypes = pluginAssemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface).ToList();

            List<IPlugin> plugins = new List<IPlugin>();

            foreach (var type in pluginTypes)
            {
                IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                plugins.Add(plugin);
            }

            Console.WriteLine("Found {0} plugins.", plugins.Count);
            
            foreach(var plugin in plugins)
            {
                Console.WriteLine($"Executing{plugin.Name}");
                plugin.Execute();
            }

        }
    }
}