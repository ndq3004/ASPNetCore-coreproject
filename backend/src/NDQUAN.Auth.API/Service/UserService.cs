using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NDQUAN.Auth.API.Helpers;
using NDQUAN.Auth.API.IService;
using NDQUAN.Core.DL;
using NDQUAN.Core.DL.Infrastructure;
using NDQUAN.Core.DL.IServices;
using NDQUAN.Core.DL.Services;
using NDQUAN.Core.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NDQUAN.Auth.API.Service
{
    public class UserService : IUserService
    {
        private readonly IDatabaseService _db;
        private readonly GlobalConfig _appSettings;

        public UserService(
            IOptions<GlobalConfig> appSettings,
            IDatabaseService db
        )
        {
            _appSettings = appSettings.Value;
            _db = db;
        }

        public IEnumerable<user_member> GetAll()
        {
            var cnn = DLConnection.GetDemoConnection();
            return DLCommand.QueryCommandText<user_member>(cnn, "select * from core.\"user_member\";");
        }

        public user_member GetById(Guid id)
        {
            var cnn = DLConnection.GetDemoConnection();
            return DLCommand.QueryCommandText<user_member>(cnn, $"select * from core.\"user_member\" where user_id = '{id}';").FirstOrDefault();
        }

        /// <summary>
        /// Register an user
        /// </summary>
        /// <param name="registerInfo"></param>
        /// <returns></returns>
        public bool Register(RegisterModel registerInfo)
        {
            var cnn = DLConnection.connectionString;
            using (var db = _db.GetContext<UserContext>(cnn))
            {
                user_member user = new user_member(registerInfo);

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(registerInfo.password, out passwordHash, out passwordSalt);
                user.password_hash = passwordHash;
                user.password_salt = passwordSalt;

                db.user_member.Add(user);
                int success = db.SaveChanges();
                if (success == 1) return true;
            }
            return false;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be empty!");

            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Verify password by comparing each hashed byte
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be empty!");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of passwordHash");
            if (storedSalt.Length != 64) throw new ArgumentException("Invalid length of passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}
