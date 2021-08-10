using Microsoft.EntityFrameworkCore;
using NDQUAN.Core.DL.Infrastructure.Base;
using NDQUAN.Core.Models.AuthModels;

namespace NDQUAN.Core.DL.Infrastructure
{
    public class UserContext : BaseDataContext
    {
        public UserContext(string connectionString) : base(connectionString) { }
        public DbSet<user_member> user_member { get; set; }
    }
}
