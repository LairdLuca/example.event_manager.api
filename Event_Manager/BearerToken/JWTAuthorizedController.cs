using Event_Manager.SharedFunctions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Event_Manager.BearerToken
{
    public abstract class JWTAuthorizedController : HelperController
    {
        public const string _ID = "id";
        public const string _EMAIL = "email";
        public const string _ADMIN = "isAdmin";

        internal Guid GetCurrentAccountGUID()
        {
            string guid = HttpContext.User.Claims.FirstOrDefault(x => x.Type == _ID)?.Value;

            if (!string.IsNullOrEmpty(guid))
                return Guid.Parse(guid);

            throw new Exception("Token not valid");
        }

        internal string GetCurrentAccountEmail()
        {
            string email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == _EMAIL)?.Value;

            if (!string.IsNullOrEmpty(email))
                return email;

            throw new Exception("Token not valid");
        }

        internal bool GetCurrentAccountIsAdmin()
        {
            string isAdmin = HttpContext.User.Claims.FirstOrDefault(x => x.Type == _ADMIN)?.Value;

            if (!string.IsNullOrEmpty(isAdmin))
                return bool.Parse(isAdmin);

            throw new Exception("Token not valid");
        }

    }
}
