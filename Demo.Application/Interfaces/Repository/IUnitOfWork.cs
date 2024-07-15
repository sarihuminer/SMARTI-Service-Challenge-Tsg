using Demo.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        Task<EntitiesResponse> SaveChangesAsync();

        void Commit();

        void Rollback();
    }
}
