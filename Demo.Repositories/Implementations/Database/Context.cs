using Demo.Domain.Enums;
using Demo.Domain.Model.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Demo.Repositories.Implementations.Database
{
    public class Context :DbContext
    {
        public Db Dbname {  get; }

        private readonly IConfiguration _configuration;

        private IConnectionStrings _connectionStrings { get; set; }

        public Context(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<Party> Parties { get; set; }
    
    }

}
