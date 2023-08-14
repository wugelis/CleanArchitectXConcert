using Domain.ConcertTickets;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ConcertTickets
{
    public class ReserveResponseDTO
    {
        public SeatReservation MySeatServe { get; set; }
        public Ticket MyTicket { get; set; }
    }
}
