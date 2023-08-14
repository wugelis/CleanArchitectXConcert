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
        public ShowTime ShowTime { get; set; }
        // 確認與預訂票卷
        public Ticket SetReservat(Ticket ticket)
        {
            return ticket;
        }
        public string[] GetReserveByDate(TimeSpan reserveRange)
        {
            return new string[] { };
        }
        public static SeatReservation Create(string ReserveName, ShowTime ReserveShowTime)
        {
            return new SeatReservation() { Id = Guid.NewGuid(), ShowTime = ReserveShowTime };
        }
        // 檢核購票時間、並檢核選擇票種是否還有位子？（如預定是空位，則傳回：true; 否則傳回：false）
        public bool CheckVenueIsExist()
        {
            return true;
        }
    }
}