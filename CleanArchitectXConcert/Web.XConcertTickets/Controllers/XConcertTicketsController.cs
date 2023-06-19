using EasyArchitect.OutsideApiControllerBase;
using EasyArchitect.OutsideManaged.AuthExtensions.Attributes;
using EasyArchitect.OutsideManaged.AuthExtensions.Filters;
using EasyArchitect.OutsideManaged.AuthExtensions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mxic.FrameworkCore.Core;
using System;

namespace Web.XConcertTickets.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class XConcertTicketsController : OutsideBaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUriExtensions _uriExtensions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userService"></param>
        /// <param name="httpContextAccessor"></param>
        public XConcertTicketsController(
            ILogger<OutsideBaseApiController> logger, 
            IUserService userService, 
            IUriExtensions uriExtensions,
            IHttpContextAccessor httpContextAccessor) 
            : base(logger, userService, httpContextAccessor)
        {
            _userService = userService;
            _uriExtensions = uriExtensions;
        }

        /// <summary>
        /// 範例程式（需要驗證）
        /// </summary>
        /// <returns></returns>
        [NeedAuthorize]
        [HttpGet]
        [APIName("GetPersons")]
        [ApiLogException]
        [ApiLogonInfo]
        public async Task<IEnumerable<Person>> GetPersons()
        {
            return await Task.FromResult(new Person[]
            {
                new Person()
                {
                    ID = 1,
                    Name = "Gelis Wu"
                }
            });
        }

        /// <summary>
        /// 取得 Current Identity Id
        /// </summary>
        /// <returns></returns>
        [NeedAuthorize]
        [APIName("GetIdentityId")]
        [ApiLogException]
        [ApiLogonInfo]
        public async Task<decimal?> GetIdentityId()
        {
            return await Task.FromResult(_userService.IdentityId);
        }

        /// <summary>
        /// 取得 Current Identity Id
        /// </summary>
        /// <returns></returns>
        [NeedAuthorize]
        [APIName("GetIdentityUser")]
        [ApiLogException]
        [ApiLogonInfo]
        public async Task<string> GetIdentityUser()
        {
            return await Task.FromResult(_userService.IdentityUser);
        }
    }

    /// <summary>
    /// 範例程式：請放置在你的 Models/Dto/VO 專案裡
    /// </summary>
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
