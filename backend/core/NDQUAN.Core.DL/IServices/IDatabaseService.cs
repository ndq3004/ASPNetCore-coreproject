using System;
using System.Collections.Generic;
using System.Text;

namespace NDQUAN.Core.DL.IServices
{
    public partial interface IDatabaseService
    {
        public TContext GetContext<TContext>(string connectionString);
    }
}
