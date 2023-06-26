using Domain.ConcertTickets;

namespace Domain
{
    public class Ticket: Entity, IAggregateRoot
    {
        public Guid Id { get; set; }
        public string ReservatName { get; protected set; }
        // 確認購票
        public int SaveTicket()
        {
            return 0;
        }
        // 建立票卷
        public static Ticket Create(SeatReservation reserve)
        {
            return new Ticket() { Id = Guid.NewGuid() };
        }
    }
}