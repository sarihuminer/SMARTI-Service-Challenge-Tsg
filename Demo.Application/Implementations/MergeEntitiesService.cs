using Demo.Application.Interfaces;
using Demo.Domain.Model.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.Application.Implementations
{
    public class MergeEntitiesService : IMergeEntitiesService
    {

        private readonly IPrioritySettingsService _prioritySettingsService;

        public MergeEntitiesService(IPrioritySettingsService prioritySettingsService)
        {
            _prioritySettingsService = prioritySettingsService;
        }

        public List<Dictionary<string, object>> MergeEntities(List<Entity> entities)
        {
            if (!entities.Any())
                return null;

            List<Dictionary<string, object>> mergeEntities = new List<Dictionary<string, object>>();
            List<string> typeOfEnties = _prioritySettingsService.GetKeysOfPrioritySettings();

            foreach (var type in typeOfEnties)
            {
                try
                {
                    var prioritySettings = _prioritySettingsService.GetPrioritySettings(type);
                    if (prioritySettings == null)
                        return null;
                    Dictionary<string, object> mergeItem = MergeFields(entities, prioritySettings.Priorities, type);
                    mergeEntities.Add(mergeItem);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return mergeEntities;
        }

        private Dictionary<string, object> MergeFields(List<Entity> entities, Dictionary<string, object> priorities, string type)
        {
            var result = new Dictionary<string, object>();

            foreach (var field in priorities)
            {
                if (field.Value is List<string> sources)
                {
                    foreach (var source in sources)
                    {
                        var entity = entities.FirstOrDefault(e => e.Source == source && e.EntityType == type && e.Fields.ContainsKey(field.Key));
                        if (entity != null)
                        {
                            result[field.Key] = entity.Fields[field.Key];
                            break;
                        }
                    }
                }
                else if (field.Value is Dictionary<string, List<string>> nestedPriorities)
                {
                    var nestedEntities = entities
                        .Where(e => e.Fields.ContainsKey(field.Key) && e.Fields[field.Key] is JsonElement)
                        .Select(e => new Entity
                        {
                            Source = e.Source,
                            EntityType = e.EntityType,
                            Fields = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(((JsonElement)e.Fields[field.Key]).GetRawText())
                        })
                        .ToList();
                    result[field.Key] = MergeFields(nestedEntities, nestedPriorities.ToDictionary(pair => pair.Key, pair => (object)pair.Value), type);
                }
            }

            return result;
        }
    }


}
