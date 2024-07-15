using Demo.Application.Interfaces.Repository;
using Demo.Domain.Model;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repositories.Implementations.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public Context Db { get; }

        private Dictionary<string, dynamic> _repositories;

        private readonly ILogger _logger;

        public UnitOfWork(Context db, ILogger logger)
        {
            Db = db;
            _logger = logger;
        }

        //public IRepository<TEntity> Repository<TEntity>()
        //{
        //    if (_repositories == null)
        //        _repositories = new Dictionary<string, dynamic>();
        //    var type
        //}

        public void Commit()
        {
           // _dbContext.SaveChanges();
        }

        public void Rollback()
        {
            // Rollback logic here
        }

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public Task<EntitiesResponse> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
