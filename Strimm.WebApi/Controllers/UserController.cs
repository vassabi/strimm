using Newtonsoft.Json.Linq;
using Strimm.Model;
using Strimm.WebApi.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Strimm.WebApi.Controllers
{
    public class UserController : BaseApiController
    {
        // GET: api/User
        protected IEnumerable<User> GetAllUsers()
        {
            var users = new List<User>();
            users.Add(new User()
            {
                UserId = 1,
                UserName = "stolmax",
                AccountNumber = "123343",
                IsDeleted = false,
                CreatedDate = DateTime.Now.AddDays(-45)
            });

            return users.ToList();
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await Task.FromResult(GetAllUsers());
        }
    }
}
