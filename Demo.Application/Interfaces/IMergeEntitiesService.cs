using Demo.Domain.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interfaces
{
    public interface IMergeEntitiesService
    {
        List<Dictionary<string, object>> MergeEntities(List<Entity> entities);
    }
}
