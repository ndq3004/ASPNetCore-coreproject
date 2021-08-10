using Microsoft.EntityFrameworkCore;
using NDQUAN.Core.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NDQUAN.Core.DL.Infrastructure.Base
{
    public class BaseDataContext : DbContext
    {
        private readonly string _connectionString = "";
        public BaseDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_connectionString);

    }
}
