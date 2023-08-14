using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ConcertTickets
{
    /// <summary>
    /// 無效的訂位紀錄（可能該座位已經被預訂；或者是資料輸入有誤）
    /// </summary>
    public class InvalidSeatReservationException : Exception
    {
        public InvalidSeatReservationException(string message) : base(message) { }
    }
}
