using Demo.Application.Interfaces;
using Demo.Domain.Model.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
            var json = await File.ReadAllTextAsync(_path);
            var settings = JsonConvert.DeserializeObject<PrioritySettings>(json);
            _prioritySettings = settings.Entities.ToDictionary(e => e.EntityType);
        }

        public EntityPriority GetPrioritySettings(string entityType)
        {
            return _prioritySettings.TryGetValue(entityType, out var priority) ? priority : null;
        }
    }
}
