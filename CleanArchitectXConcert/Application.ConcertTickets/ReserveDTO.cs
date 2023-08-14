using Domain.ConcertTickets;

namespace Application.ConcertTickets
{
    public class ReserveDTO
    {
        public string ReserveID { get; set; }
        public DateTime? ReserveTime { get; set; }
        public ShowTime ShowTime { get; set; }
        public string ReserveName { get; set; }
    }
}