using Domain;
using Domain.ConcertTickets;

namespace Application.ConcertTickets
{
    /// <summary>
    /// X宏網路訂票系統 - Repository 儲存體
    /// </summary>
    public interface IReserveRepository
    {
        /// <summary>
        /// 查詢票卷 Tickets 資訊
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        IEnumerable<Ticket> GetMySeatRevervations(Guid ticketId);
        /// <summary>
        /// 儲存票卷 Tickets 資訊
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        int SaveConcertReservation(Ticket ticket);
    }
}