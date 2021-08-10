using NDQUAN.Core.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDQUAN.Auth.API.IService
{
    public interface IAuthService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}
