using NDQUAN.Core.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDQUAN.Auth.API.IService
{
    public interface IUserService
    {
        IEnumerable<user_member> GetAll();
        user_member GetById(Guid id);
        bool Register(RegisterModel user);
    }
}
