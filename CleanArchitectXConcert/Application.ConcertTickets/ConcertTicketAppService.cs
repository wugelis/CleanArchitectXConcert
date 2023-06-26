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
        // 購票作業
        public int Reservation(ReserveDTO ticketDto)
        {
            // 檢核購票時間

            // 檢核選擇票種是否還有位子？

            // 若選擇時間有票種，進行購票作業（此預定保留 10 分鐘、若未付款，10分鐘取消資格，將釋放此預訂票種給其他訂票作業的 Transaction）
            SeatReservation seat = SeatReservation.Create(ticketDto.ReserveID);

            // 預訂票卷（此預定預設保留 10 分鐘）
            Ticket ticket = seat.SetReservat(ticketDto.ReserveID);

            return 0;
        }
        // 確認定位
        public int SaveReservation(Ticket ticket)
        {
            return _reserveRepository.SaveConcertReservation(ticket);
        }
    }
}