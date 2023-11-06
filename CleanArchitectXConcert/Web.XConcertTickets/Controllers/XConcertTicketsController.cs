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
using System.Collections;

namespace Web.XConcertTickets.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class XConcertTicketsController : OutsideBaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUriExtensions _uriExtensions;
        private readonly ConcertTicketAppService _ticketApp;

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
            IHttpContextAccessor httpContextAccessor,
            ConcertTicketAppService ticketApp) 
            : base(logger, userService, httpContextAccessor)
        {
            _userService = userService;
            _uriExtensions = uriExtensions;
            _ticketApp = ticketApp;
        }

        /// <summary>
        /// 訂票作業
        /// </summary>
        /// <param name="ReserveName"></param>
        /// <param name="startShowTime"></param>
        /// <param name="endShowTime"></param>
        /// <returns></returns>
        //[NeedAuthorize]
        [HttpPost]
        [APIName("SeatReservation")]
        [ApiLogException]
        [ApiLogonInfo]
        public async Task<ReserveResponseDTO> SeatReservation(ReserveRequestDTO reserveRequest)
        {
            //Application.ConcertTickets.ConcertTicketAppService app = new ConcertTicketAppService(new ReserveRepository());
            ReserveResponseDTO result = _ticketApp.Reservation(
                new ReserveDTO()
                {
                    ReserveID = "XXXXX10001",   //訂位代號（流水號）
                    ReserveName = reserveRequest.ReserveName,  //定位大名
                    ReserveTime = DateTime.Now, // 訂位時間、不等於票種時間 或 演唱會場時間 (the showtime for Concert Venue)
                    ShowTime = new ShowTime(reserveRequest.startShowTime, reserveRequest.endShowTime)
                }
            );

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 保存目前 Current Session 下的訂票紀錄
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [APIName("SaveReservation")]
        [ApiLogException]
        [ApiLogonInfo]
        public async Task<int> SaveReservation(TicketRequestDTO ticketDTO)
        {
            return await Task.FromResult(_ticketApp.SaveReservation(ticketDTO));
        }

        [HttpPost]
        [APIName("SaveData")]
        [ApiLogException]
        [ApiLogonInfo]
        public async Task<int> SaveData(Person data)
        {
            var ticket = data;
            return await Task.FromResult(1);
        }
        /// <summary>
        /// 取得目前 Current Session 下的訂票情況
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        [APIName("GetTicketsById")]
        [ApiLogException]
        [ApiLogonInfo]
        public async Task<IEnumerable<TicketResponseDTO>> GetTicketsById(string grid)
        {
            return await Task.FromResult(_ticketApp.GetTickets(Guid.Parse(grid)));
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
