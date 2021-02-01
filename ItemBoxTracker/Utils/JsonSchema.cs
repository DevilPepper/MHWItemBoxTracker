using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;

namespace MHWItemBoxTracker.Utils
{
    public class JsonSchema
    {
        public static string Generate<T>()
        {
            var generator = new JSchemaGenerator();
            generator.DefaultRequired = Required.DisallowNull;
            var json = generator.Generate(typeof(T));
            return json.ToString();
        }
    }
}
