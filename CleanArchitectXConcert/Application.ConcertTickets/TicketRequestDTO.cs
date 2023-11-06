using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ConcertTickets
{
    /// <summary>
    /// 傳入有效的票卷
    /// </summary>
    public class TicketRequestDTO
    {
        /// <summary>
        /// 我的票卷
        /// </summary>
        public Ticket MyTicket { get; set; }
    }
}
