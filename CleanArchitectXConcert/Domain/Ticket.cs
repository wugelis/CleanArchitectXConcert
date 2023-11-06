using Domain.ConcertTickets;

namespace Domain
{
    public class Ticket : Entity, IAggregateRoot
    {
        private SeatReservation _seatReservation;
        public SeatReservation SeatReservationInfo
        {
            get => _seatReservation;
            set => _seatReservation = value;
        }
        /// <summary>
        /// Entity ID
        /// </summary>
        public Guid Id { get; set; }
        //public string ReserveName { get; set; } // 與 SeatReservation 中的 ReserveName 重複
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