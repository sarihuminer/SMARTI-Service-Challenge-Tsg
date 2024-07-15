using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interfaces.Repository
{
    public class IncludeExp<TEntity>
    {
        public Expression<Func<TEntity, object>> Include { get; set; }

        public Expression<Func<object, object>> ThenInclude { get; set; }

        public Expression<Func<object, object>> Then2Include { get; set; }

        public IncludeExp(Expression<Func<TEntity, object>> include, Expression<Func<object, object>> thenInclude = null, Expression<Func<object, object>> thenInclude2 = null, Expression<Func<object, object>> then2Include = null)
        {
            Include = include;
            ThenInclude = thenInclude;
            Then2Include = then2Include;
        }
    }
}
