using EasyArchitect.OutsideManaged.AuthExtensions.Models;
using EasyArchitect.OutsideManaged.AuthExtensions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ConcertTickets
{
    /// <summary>
    /// 售票系統 - 會員登入服務
    /// </summary>
    public class AccountTicketAppService
    {
        private readonly IUserService _userService;

        public AccountTicketAppService(IUserService userService) 
        {
            _userService = userService;
        }

        /// <summary>
        /// 使用套件功能 (AuthExtensions 的 Login 登入功能) 進行登入作業
        /// </summary>
        /// <param name="authenticateModel"></param>
        /// <returns></returns>
        public async Task<AuthenticateResponse> Login(AuthenticateRequest authenticateModel)
        {
            var result = await Task.FromResult(_userService.Authenticate(authenticateModel));

            return result;
        }
        /// <summary>
        /// 取得目前登入的使用者名稱
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetCurrentUser()
        {
            var result = await Task.FromResult(_userService.IdentityUser);

            return result;
        }
    }
}
