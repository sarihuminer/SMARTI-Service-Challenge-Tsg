using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Model.Data
{
    public class EntityPriority
    {
        public string EntityType { get; set; }
        public Dictionary<string, object> Priorities { get; set; }

        [JsonConstructor]
        public EntityPriority(string entityType, Dictionary<string, JToken> priorities)
        {
            EntityType = entityType;
            Priorities = priorities.ToDictionary(
                kvp => kvp.Key,
                kvp => ConvertJToken(kvp.Value)
            );
        }

        private static object ConvertJToken(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Array:
                    return token.ToObject<List<string>>() ?? new List<string>();
                case JTokenType.Object:
                    return token.ToObject<Dictionary<string, List<string>>>() ?? new Dictionary<string, List<string>>();
                default:
                    throw new InvalidOperationException("Unexpected token type: " + token.Type);
            }
        }
    }
}
