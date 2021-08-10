using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace NDQUAN.Core.Models.AuthModels
{
    [Table("user_member", Schema = "core")]
    public class user_member
    {
        [Key]
        public Guid user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string email { get; set; }

        [JsonIgnore]
        public byte[] password_hash { get; set; }
        public byte[] password_salt { get; set; }

        public user_member() {  }
        public user_member(RegisterModel registerInfo)
        {
            user_id = Guid.NewGuid();
            first_name = registerInfo.first_name;
            last_name = registerInfo.last_name;
            username = registerInfo.username;
            email = registerInfo.email;
        }
    }
}
