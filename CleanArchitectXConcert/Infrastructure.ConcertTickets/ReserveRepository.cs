using Application.ConcertTickets;
using Domain;

namespace Infrastructure.ConcertTickets
{
    public class ReserveRepository : IReserveRepository
    {
        public int SaveConcertReservation(Ticket ticket)
        {
            return ticket.SaveTicket();
        }
    }
}