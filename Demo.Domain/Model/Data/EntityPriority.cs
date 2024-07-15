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
    }
}
