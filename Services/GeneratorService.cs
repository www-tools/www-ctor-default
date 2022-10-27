using System.Text;

namespace WwwCtorDefault.Services
{
    public class GeneratorService
    {
        public string GetData(string data)
        {
            var sb = new StringBuilder();
            var splittedInitialData = data.Split(new string[] { "\n" }, StringSplitOptions.None);

            var className = string.Empty;
            var propertiesForCtor = new List<string>();

            foreach (var item in splittedInitialData)
            {
                var trimmedString = item.Trim();

                if (trimmedString.Contains(" class "))
                {
                    var splittedItem = trimmedString.Split(" ");
                    className = splittedItem[2];
                }

                if (trimmedString.Contains("{ get; set; }"))
                {

                    var splittedItem = trimmedString.Split(" ");
                    var propertyName = splittedItem[2];

                    propertiesForCtor.Add(propertyName);
                }
            }

            sb.AppendLine($"public {className}()");
            sb.AppendLine($"{{");

            foreach (var property in propertiesForCtor)
            {
                sb.AppendLine($"\tthis.{property} = default;");
            }

            sb.AppendLine($"}}");

            return sb.ToString();
        }
    }
}
