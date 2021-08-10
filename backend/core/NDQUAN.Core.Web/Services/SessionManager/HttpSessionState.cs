using System;
using System.Collections;
using System.Collections.Generic;

namespace NDQUAN.Core.Web.Services.SessionManager
{
    public class SessionData
    {
        public string session_id { get; set; }

        public UserInfo user_info { get; set; }
        public DateTime login_time { get; set; } 
        
        public DateTime expired_date { get; set; }

        public SessionData()
        {
            login_time = DateTime.Now;
            expired_date = DateTime.Now.AddMinutes(2);
        }

    }

    public class UserInfo
    {
        public Guid user_id { get; set; }
    }
}
