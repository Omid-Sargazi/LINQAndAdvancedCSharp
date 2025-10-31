using ReflectionProblems.Problem1;

namespace PluginContract;

public delegate bool PluginFilter(IPlugin plugin);
public class HelloPlugin : IPlugin
{
    public string Name => "Hello World Plugin";

    public void Execute()
    {
        Console.WriteLine("Hello from the first plugin!");
    }
}
