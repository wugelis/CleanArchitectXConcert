using Domain.ConcertTickets;

namespace Domain
{
    public class Ticket : Entity, IAggregateRoot
    {
        private SeatReservation _seatReservation;
        public SeatReservation SeatReservationInfo => _seatReservation;
        public Guid Id { get; set; }
        public string ReservatName { get; protected set; }
        // 確認購票
        public int SaveTicket()
        {
            return 0;
        }
        // 建立票卷實體
        public static Ticket Create(SeatReservation reserve)
        {
            return new Ticket() { Id = Guid.NewGuid(), _seatReservation = reserve };
        }
    }
}