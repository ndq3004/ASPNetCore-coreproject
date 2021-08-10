using NDQUAN.Core.DL.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace NDQUAN.Core.DL.Services
{
    public partial class DatabaseService : IDatabaseService
    {
        public TContext GetContext<TContext>(string connectionString)
        {
            var context = (TContext)Activator.CreateInstance(typeof(TContext), connectionString);
            return context;
        }
    }
}
