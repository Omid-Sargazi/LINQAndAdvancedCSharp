using System.Reflection;
using System.Text;

namespace Reflection1.Problems
{
    public class User
    {
        public string Name { get; set; } = "Omid";
        public int Age { get; set; } = 42;
        public bool IsDeveloper { get; set; } = true;
        public decimal? Salary { get; set; } = null;
    }

    public class SimpleJsonSerializer
    {
        public static string ToJson(object obj)
        {
            var type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();

            StringBuilder sb = new StringBuilder();
            sb.Append('{');
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                var name = prop.Name;
                var value = prop.GetValue(obj);

                string formattedValue = value switch

                {
                    string s => $"\"{s}\"",
                    null => "null",
                    bool b => b.ToString().ToLower(),
                    _ => value.ToString().ToLower()
                };

                sb.Append($"\"{name}\":{formattedValue}");

                if (i < props.Length - 1)
                {
                    sb.Append(',');
                }
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}