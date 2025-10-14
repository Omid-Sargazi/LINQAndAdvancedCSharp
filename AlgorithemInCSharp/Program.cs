using AlgorithemInCSharp.LeetcodeExample;
using AlgorithemInCSharp.Patterns;
using AlgorithemInCSharp.Patterns.BehavioralDesignPattern;
using AlgorithemInCSharp.Reflections;
using AlgorithemInCSharp.RpositoryPattern;

// Example.Run();

// ClientMediator.Run();

// TestMakeGeneruc.TestAssignableFrom();

// CommandClient.Run();
// await NotificationClient.Run();
// LSubstringWithoutRepeatingCharacters.Run("Omidss");
// ClientLits.Run();
// Deconstructt d1 = new Deconstructt("Omid", 42);
// var (name, age) = d1;
// Console.WriteLine(name + age);

var factory = new TaxStrategyFactory();
var germanTax = factory.GetTaxStrategy(Country.German);

ClientTax cli = new ClientTax(germanTax);

Console.WriteLine($"{cli.CalTax(45)}");