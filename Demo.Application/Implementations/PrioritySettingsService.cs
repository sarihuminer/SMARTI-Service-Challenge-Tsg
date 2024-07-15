using Demo.Application.Interfaces;
using Demo.Domain.Model.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Implementations
{
    public class PrioritySettingsService : IPrioritySettingsService
    {
        private Dictionary<string, EntityPriority> _prioritySettings;

        private readonly string _path;

        public PrioritySettingsService(IConfiguration configuration)
        {
            _path = configuration.GetSection("PrioritySettingsFile").Value;
        }

        public async Task LoadPrioritySettingsAsync()
        {
            try
            {
                var json = await File.ReadAllTextAsync(_path);
                string data = JObject.Parse(json)["priorities"].ToString();
                var settings = JsonConvert.DeserializeObject<List<EntityPriority>>(data);
                _prioritySettings = settings.ToDictionary(e => e.EntityType);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public EntityPriority GetPrioritySettings(string entityType)
        {
            return _prioritySettings.TryGetValue(entityType, out var priority) ? priority : null;
        }

        public List<string> GetKeysOfPrioritySettings()
        {
           return new List<string>(_prioritySettings.Keys);
        }
    }
}
