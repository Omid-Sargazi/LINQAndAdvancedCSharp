using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime;

namespace DesignPattern.Reflections
{
    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public double Divide(double a, double b) => a / b;
        public string Greet(string name) => $"Hello, {name}!";
        public void PrintMessage(string message) => Console.WriteLine($"Message: {message}");
    }

    public class CalReflection
    {
        public static void Execute()
        {
            var calculator = new Calculator();
            Type calType = calculator.GetType();

            string methodName = "Add";
            MethodInfo method = calType.GetMethod(methodName);

            if (method == null)
            {
                Console.WriteLine($"Method {methodName} not Found.");
                return;
            }

            object[] parameters = new object[] { 2, 3 };

            try
            {
                object result = method.Invoke(calculator, parameters);
                Console.WriteLine($"Result {result}");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error:{ex.Message}");
            }
        }

        public enum Format

        {
            PDF,
            Word,
            Image,
        }

        public class Document
        {
            public Format Format { get; set; }
            public string Context { get; set; }
            public Document(Format format, string context)
            {
                Format = format;
                Context = context;
            }
        }

        public interface IConverter
        {
            bool CanConvert(Format from, Format to);
            Document Convert(Document document, Format to);
        }

        public class PdfToWordConverter : IConverter
        {
            public bool CanConvert(Format from, Format to)
            {
                return from == Format.PDF && to == Format.Word;
            }

            public Document Convert(Document document, Format to)
            {
                Console.WriteLine("Converting PDF -> Word");
                return new Document(Format.Word, document.Context + "(converted to Word)");
            }
        }

        public class WordToPdfConverter : IConverter
        {
            public bool CanConvert(Format from, Format to)
            {
                return from == Format.Word && to == Format.PDF;
            }

            public Document Convert(Document document, Format to)
            {
                Console.WriteLine("Converting Word -> PDF");
                return new Document(Format.PDF, document.Context + "converted to pdf");
            }
        }

        public class PdfToImageConverter : IConverter
        {
            public bool CanConvert(Format from, Format to)
            {
                return from == Format.PDF && to == Format.Image;
            }

            public Document Convert(Document document, Format to)
            {
                Console.WriteLine("Converting PDF -> Image");
                return new Document(Format.PDF, document + "Converted to Image");
            }
        }

        public class ConverterService
        {
            private List<IConverter> _converters = new List<IConverter>();

            public void Register(IConverter converter)
            {
                if (converter == null) throw new ArgumentNullException(nameof(converter));
                _converters.Add(converter);
            }

            public void Unregister(IConverter converter)
            {
                if (converter == null) return;
                _converters.Remove(converter);
            }

            public Document Convert(Document doc, Format to)
            {
                foreach (var c in _converters)
                {
                    if (c.CanConvert(doc.Format, to))
                    {
                        try
                        {
                            return c.Convert(doc, to);
                        }
                        catch (System.Exception ex)
                        {

                            Console.WriteLine($"Converter Failed:{ex.Message}");
                        }
                    }
                }
                Console.WriteLine($"No Converter Avilable for {doc.Format}+=>{to}");
                return null;
            }
        }
    }

    public enum Discount
    {
        Season,
        Percentage,
        Fixed,
    }

    public interface IDiscount
    {
        void ApplyDiscount(decimal amount);
    }

    public class SeasonalDiscount : IDiscount
    {
        public void ApplyDiscount(decimal amount)
        {
            Console.WriteLine($"Season Discount:{amount - amount * .9m}");
        }
    }

    public class FixedAmountDiscount : IDiscount
    {
        public void ApplyDiscount(decimal amount)
        {
            Console.WriteLine($"Season Discount:{amount - amount * .8m}");

        }
    }

    public class PercentageDiscount : IDiscount
    {
        public void ApplyDiscount(decimal amount)
        {
            Console.WriteLine($"Season Discount:{amount - amount * .7m}");

        }
    }

    public class SystemDiscount
    {
        private decimal Amount;
        private List<IDiscount> _discounts = new List<IDiscount>();

        public SystemDiscount(decimal amount)
        {
            Amount = amount;
        }

       public  void Execute()
         {
            foreach(var discount in _discounts)
            {
                discount.ApplyDiscount(Amount);
            }
        }


    }

}