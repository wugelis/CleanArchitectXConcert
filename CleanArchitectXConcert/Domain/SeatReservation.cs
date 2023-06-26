namespace Domain.ConcertTickets
{
    /// <summary>
    /// 
    /// </summary>
    public class SeatReservation : Entity
    {
        public Guid Id { get; protected set; }
        public string ReserveName { get; protected set; }
        public ConcertVenue ReserveConcertVenue { get; protected set; }
        public DateTime? ShowTime { get; set; }
        // 確認與預訂票卷
        public Ticket SetReservat(string name)
        {
            return new Ticket();
        }
        public string[] GetReserveByDate(TimeSpan reserveRange)
        {
            return new string[] { };
        }
        public static SeatReservation Create(string ReserveName)
        {
            return new SeatReservation() { Id = Guid.NewGuid() };
        }
    }
}