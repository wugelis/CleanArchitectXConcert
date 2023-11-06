using Domain.ConcertTickets;
using Domain;

namespace Application.ConcertTickets
{
    public class ConcertTicketAppService
    {
        private IReserveRepository _reserveRepository;
        public ConcertTicketAppService(IReserveRepository ticketRepository)
        {
            _reserveRepository = ticketRepository;
        }
        /// <summary>
        /// 購票作業流程
        /// </summary>
        /// <param name="ticketDto"></param>
        /// <returns></returns>
        /// <exception cref="InvalidSeatReservationException"></exception>
        public ReserveResponseDTO Reservation(ReserveDTO ticketDto)
        {
            // 建立票種、若選擇時間有票種，進行購票作業
            SeatReservation seat = SeatReservation.Create(ticketDto.ReserveName, ticketDto.ShowTime);

            seat = seat.BuildConertVenue(seat);

            // 產生新的票卷（此預定保留 10 分鐘）
            Ticket ticket = Ticket.Create(seat);

            // 檢核購票時間、並檢核選擇票種是否還有位子？
            if (!ticket.SeatReservationInfo.CheckVenueIsExist())
            {
                throw new InvalidSeatReservationException("可能該座位已經被預訂, 或者是資料輸入有誤!");
            }

            // 預訂票卷（此預定預設保留 10 分鐘、即便執行了 SeatReservat() 但若未付款，10分鐘取消資格，將釋放此預訂票種給其他訂票作業的 Transaction）
            ticket = seat.SetReservat(ticket);

            return new ReserveResponseDTO() { MySeatServe = seat, MyTicket = ticket };
        }
        /// <summary>
        /// 查詢 Currennt 訂票資訊（10分鐘內付款內有效）
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public IEnumerable<TicketResponseDTO> GetTickets(Guid ticketId)
        {
            IEnumerable<TicketResponseDTO> result = from ticket in _reserveRepository.GetMySeatRevervations(ticketId)
                                                    select new TicketResponseDTO()
                                                    {
                                                        //Id = ticket.Id,
                                                        ReserveName = ticket.SeatReservationInfo.ReserveName,
                                                        ReserveConcertVenue = ticket.SeatReservationInfo.ReserveConcertVenue,
                                                        ticket = ticket
                                                    };

            return result;
        }
        // 確認定位
        public int SaveReservation(TicketRequestDTO ticket)
        {
            return _reserveRepository.SaveConcertReservation(ticket.MyTicket);
        }
    }
}