using Domain;

namespace Application.ConcertTickets
{
    public interface IReserveRepository
    {
        int SaveConcertReservation(Ticket ticket);
    }
}