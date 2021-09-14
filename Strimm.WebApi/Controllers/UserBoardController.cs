using Strimm.Data.Repositories;
using Strimm.Model.Projections;
using Strimm.WebApi.Filters;
using Strimm.WebApi.Services;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Strimm.WebApi.Controllers
{
    [StrimmAuthorize]
    public class UserBoardController : BaseApiController
    {
        private IStrimmIdentityService identityService;

        public UserBoardController(IStrimmIdentityService identityService)
        {
            this.identityService = identityService; 
        }

        //[HttpGet]
        //[Route("api/user/userboard/{id:int}", Name = "UserBoardById")]
        //public async Task<UserBoard> GetUserboardById(int id)
        //{
        //    return await Task.Run<UserBoard>(() => 
        //    {
        //        return BoardManage.GetUserBoardDataByUserId(id);
        //    });
        //}

        [HttpGet]
        [Route("api/user/userboard/{name}", Name = "UserBoardByName")]
        public async Task<UserBoard> GetUserboardByName(string name, DateTime clientTime)
        {
            return await Task.Run<UserBoard>(() => 
            {
                return BoardManage.GetUserBoardDataByUserName(name, clientTime);
            });
        }

        [HttpPut]
        [HttpPatch]
        public async Task<UserBoard> Put([FromBody] UserBoard userBoard)
        {
            return await Task.Run<UserBoard>(() =>
            {
                return BoardManage.UpdateUserBoard(userBoard.UserId, userBoard.BoardName, userBoard.ProfileImageUrl, userBoard.BackgroundImageUrl, userBoard.UserStory);
            });
        }
    }
}
