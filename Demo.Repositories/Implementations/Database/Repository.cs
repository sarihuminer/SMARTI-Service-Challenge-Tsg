using Demo.Application.Interfaces.Repository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repositories.Implementations.Database
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private Context DbContext;

        private readonly DbSet<TEntity> _dbSet;
        private readonly ILogger _logger;

        public Repository(Context dbContext, ILogger logger)
        {
            DbContext = dbContext;
            _logger = logger;
            _dbSet = dbContext.Set<TEntity>();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where, IEnumerable<IncludeExp<TEntity>> includeExpressions = null)
        {
            return GetQuery(where, includeExpressions).Where(where).FirstOrDefault();
        }

        private IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> where, IEnumerable<IncludeExp<TEntity>> includeExpressions = null)
        {
            IQueryable<TEntity> query = _dbSet;

            includeExpressions?.ToList().ForEach(includeExpression =>
            {
                if (includeExpression.ThenInclude != null)
                    query = query.Include(includeExpression.Include)
                                 .ThenInclude(includeExpression.ThenInclude);
                else
                    query = query.Include(includeExpression.Include);
                });
            return query;
        }
        
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
