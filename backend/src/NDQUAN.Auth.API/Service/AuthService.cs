using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NDQUAN.Auth.API.Helpers;
using NDQUAN.Auth.API.IService;
using NDQUAN.Core.DL;
using NDQUAN.Core.DL.Infrastructure;
using NDQUAN.Core.DL.IServices;
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
    public class AuthService : IAuthService
    {
        private readonly GlobalConfig _appSettings;
        private readonly IDatabaseService _db;

        public AuthService(IOptions<GlobalConfig> appSettings, IDatabaseService db)
        {
            _appSettings = appSettings.Value;
            _db = db;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            LogDB.LogDuration($"_db=null: {_db == null}");
            using (var db = _db.GetContext<UserContext>(_appSettings.UserDatabaseConnectionString))
            {
                LogDB.LogDuration($"first: {true}");
                var user = db.user_member.FirstOrDefault(u => u.username == model.Username);
                if(user != null && VerifyPasswordHash(model.Password, user.password_hash, user.password_salt))
                {
                    var token = GenerateJwtToken(user);
                    return new AuthenticateResponse(user, token);
                }
            }
            return null;
        }

        private string GenerateJwtToken(user_member user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.user_id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}
