using System;
using System.Collections.Generic;
using System.Text;

namespace NDQUAN.Core.Models.AuthModels
{
    public class AuthenticateResponse
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }
        public string Token { get; set; }

        public string SessionId { get; set; }

        public AuthenticateResponse(user_member user, string token)
        { 
            UserId = user.user_id;
            FirstName = user.first_name;
            LastName = user.last_name;
            UserName = user.username;
            Token = token;

        }
    }
}
