using System;
using System.Collections.Generic;
using System.Text;

namespace NDQUAN.Core.Models.AuthModels
{
    public class RegisterModel
    {
        public Guid user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string email { get; set; }

        public string password { get; set; }
    }
}
