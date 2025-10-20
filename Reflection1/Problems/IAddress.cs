using System.Collections;
using System.Reflection;
using System.Text;

namespace Reflection1.Problems
{
    public interface IAddress
    {
        string City { get; set; }
        string Country { get; set; }
    }

    public class Address : IAddress
    {
        public string City { get; set; } = "Luxembourg";
        public string Country { get; set; } = "Luxembourg";
    }

    public interface IJob
    {
        string Title { get; set; }
        int Salary { get; set; }
    }

    public class Job : IJob
    {
        public string Title { get; set; } = "Backend Developer";
        public int Salary { get; set; } = 6000;
    }

    public class Person2
    {
        public string Name { get; set; } = "Omid";
        public int Age { get; set; } = 42;
        public IAddress Address { get; set; } = new Address();
        public Job Job { get; set; } = new Job();
    }

    public class RecursiveJsonSerializer
    {
        public string ToJson(object obj)
        {
            if (obj == null)
                return "null";

            Type type = obj.GetType();

            if (type.IsPrimitive || obj is string || obj is decimal)
            {
                return FormatPrimitive(obj);
            }

            if (obj is IEnumerable enumerable)
            {
                return SerializeEnumerable(enumerable);
            }

            PropertyInfo[] props = type.GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append('{');

            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                var name = prop.Name;
                var value = prop.GetValue(obj);
                string serializedValue = ToJson(value);

                sb.Append($"\"{name}\":{serializedValue}");

                if (i < props.Length - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append("}");
            return sb.ToString();
        }

        private string FormatPrimitive(object value)
        {
            var formatted = value switch

            {
                string s => $"\"{s}\"",
                bool b => b.ToString().ToLower(),
                null => "null",
                _ => value.ToString().ToLower(),
            };

            return formatted;

        }

        private string SerializeEnumerable(IEnumerable items)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            bool first = true;

            foreach(var item in items)
            {
                if (!first)
                {
                    sb.Append(",");
                    sb.Append(ToJson(item));
                    first = false;
                }

            } 
                sb.Append(']');
                return sb.ToString();
        }
    }

}