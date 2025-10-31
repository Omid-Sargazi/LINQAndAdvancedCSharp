using ReflectionProblems.Problem1;

namespace PluginContract;

public class HelloPlugin : IPlugin
{
    public string Name => "Hello World Plugin";

    public void Execute()
    {
        Console.WriteLine("Hello from the first plugin!");
    }
}
