using Domain;
using Domain.ConcertTickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ConcertTickets
{
    /// <summary>
    /// Tickets Web 回傳
    /// </summary>
    public class TicketResponseDTO
    {
        public string ReserveName { get; set; }
        public ConcertVenue ReserveConcertVenue { get; set; }
        public Ticket ticket { get; set; }
    }
}
