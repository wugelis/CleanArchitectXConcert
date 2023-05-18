<Query Kind="Program">
  <Namespace>Application.DTO</Namespace>
  <Namespace>Application.ConcertTickets</Namespace>
</Query>

void Main()
{
	
}

#region Domain Layer
public interface IAggregateRoot
{
}

public class Entity
{	
}

public class Ticket: Entity, IAggregateRoot
{
	private Guid id;
	private string ReservatName;
	public void SetReservatName(string name)
	{
		ReservatName = name;
	}
	public int AddTicket(Ticket ticket)
	{
		return 0;
	}
}

public class SeatReservation: Entity
{
	
}
#endregion

#region Application Services
namespace Application.ConcertTickets
{
	public interface ITicketRepository
	{
		int AddConcertReservation(Ticket ticket);
	}
	
	public class ConcertTicketAppService
	{
		private ITicketRepository _ticketRepository;
		public ConcertTicketAppService(ITicketRepository ticketRepository)
		{
			_ticketRepository = ticketRepository;
		}
		public int Reservation(TicketDTO ticketDto)
		{
			// 檢核購票時間
			
			// 檢核選擇票種是否還有位子？
			
			// 進行購票作業
			Ticket ticket = new Ticket();
			ticket.SetReservatName("gelis");
			_ticketRepository.AddConcertReservation(ticket);
			
			return 0;
		}
	}
}

namespace Application.DTO
{
	public class TicketDTO
	{
		
	}
}
#endregion

#region Infrastructure
public class TicketRepository : ITicketRepository
{
	public int AddConcertReservation(Ticket ticket)
	{
		throw new NotImplementedException();
	}
}
#endregion