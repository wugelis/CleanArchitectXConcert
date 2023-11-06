using Application.ConcertTickets;
using Domain;
using Domain.ConcertTickets;

namespace Infrastructure.ConcertTickets
{
    /// <summary>
    /// 訂位資料 SeatReservation 的儲存體
    /// </summary>
    public class ReserveRepository : IReserveRepository
    {
        public static List<Ticket> _seatReserveTickets = new List<Ticket>();
        public ReserveRepository() { }
        /// <summary>
        /// 查詢票卷 Tickets 資訊
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Ticket> GetMySeatRevervations(Guid ticketId)
        {
            return _seatReserveTickets.Where(c => c.Id == ticketId);
        }
        /// <summary>
        /// 儲存票卷 Tickets 資訊
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public int SaveConcertReservation(Ticket ticket)
        {
            int result = 0;
            if(ticket != null)
            {
                Ticket? myticket = _seatReserveTickets
                    .Where(x => x.Id == ticket.Id)
                    .FirstOrDefault();

                if(myticket == null)
                {
                    _seatReserveTickets.Add(ticket);
                    result = 1;
                }
            }

            return result;
        }
    }
}