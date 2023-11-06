namespace Domain.ConcertTickets
{
    /// <summary>
    /// 
    /// </summary>
    public class SeatReservation : Entity
    {
        public Guid Id { get; protected set; }
        public string ReserveName { get; set; }
        public ConcertVenue ReserveConcertVenue { get; set; }
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
            return new SeatReservation() { Id = Guid.NewGuid(), ShowTime = ReserveShowTime, ReserveName = ReserveName };
        }
        // 檢核購票時間、並檢核選擇票種是否還有位子？（如預定是空位，則傳回：true; 否則傳回：false）
        public bool CheckVenueIsExist()
        {
            return true;
        }

        public SeatReservation BuildConertVenue(SeatReservation seatReservation)
        {
            seatReservation.ReserveConcertVenue = new ConcertVenue()
            {
                ShowTimeName = "音樂劇-都是奇奇惹的禍", //可來自場次的代碼檔
                ShowTimeNum = getVenue(seatReservation.ShowTime)
            };
            return seatReservation;
        }
        private int getVenue(ShowTime showTime)
        {
            return showTime.GetStartShowTime().Value.AddHours(1).Hour;
        }
    }
}