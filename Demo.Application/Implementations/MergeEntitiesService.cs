using Demo.Application.Interfaces;
using Demo.Domain.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
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

        public Dictionary<string, object> MergeEntities(List<Entity> entities)
        {
            if (!entities.Any())
                return null;

            var entityType = entities.First().EntityType;
            var prioritySettings = _prioritySettingsService.GetPrioritySettings(entityType);
            if (prioritySettings == null)
                return null;

           return MergeFields(entities, prioritySettings.Priorities);
        }

        private Dictionary<string, object> MergeFields(List<Entity> entities, Dictionary<string, object> priorities)
        {
            var result = new Dictionary<string, object>();

            foreach (var field in priorities)
            {
                if (field.Value is List<string> sources)
                {
                    foreach (var source in sources)
                    {
                        var entity = entities.FirstOrDefault(e => e.Source == source);
                        if (entity != null && entity.Fields.ContainsKey(field.Key))
                        {
                            result[field.Key] = entity.Fields[field.Key];
                            break;
                        }
                    }
                }
                else if (field.Value is Dictionary<string, object> nestedPriorities)
                {
                    var nestedEntities = entities
                        .Where(e => e.Fields.ContainsKey(field.Key) && e.Fields[field.Key] is Dictionary<string, object>)
                        .Select(e => new Entity
                        {
                            Source = e.Source,
                            EntityType = e.EntityType,
                            Fields = e.Fields[field.Key] as Dictionary<string, object>
                        })
                        .ToList();

                    result[field.Key] = MergeFields(nestedEntities, nestedPriorities);
                }
            }

            return result;
        }
    }
}
