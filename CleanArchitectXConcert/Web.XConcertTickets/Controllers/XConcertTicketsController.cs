using Application.ConcertTickets;
using Domain.ConcertTickets;
using EasyArchitect.OutsideApiControllerBase;
using EasyArchitect.OutsideManaged.AuthExtensions.Attributes;
using EasyArchitect.OutsideManaged.AuthExtensions.Filters;
using EasyArchitect.OutsideManaged.AuthExtensions.Services;
using Infrastructure.ConcertTickets;
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

        [NeedAuthorize]
        [HttpGet]
        [APIName("SeatReservation")]
        [ApiLogException]
        [ApiLogonInfo]
        public async Task<ReserveResponseDTO> SeatReservation(string ReserveName, DateTime? startShowTime, DateTime? endShowTime)
        {
            Application.ConcertTickets.ConcertTicketAppService app = new ConcertTicketAppService(new ReserveRepository());
            ReserveResponseDTO result = app.Reservation(
                new ReserveDTO()
                {
                    ReserveID = "XXXXX10001",   //訂位代號（流水號）
                    ReserveName = ReserveName,  //定位大名
                    ReserveTime = DateTime.Now, // 訂位時間、不等於票種時間 或 演唱會場時間 (the showtime for Concert Venue)
                    ShowTime = new ShowTime(startShowTime, endShowTime)
                }
            );

            return await Task.FromResult(result);
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
}
