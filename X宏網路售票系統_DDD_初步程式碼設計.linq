<Query Kind="Program">
  <Namespace>Application.DTO</Namespace>
  <Namespace>Application.ConcertTickets</Namespace>
</Query>

void Main()
{
	// Controller 用法
	Application.ConcertTickets.ConcertTicketAppService app = new ConcertTicketAppService(new ReserveRepository());
}

#region Domain Layer
public interface IAggregateRoot
{
}

public class Entity
{	
}

public abstract class ValueObject
{
	
}

public class Ticket: Entity, IAggregateRoot
{
	public Guid Id { get; set; }
	public string ReservatName { get; protected set;}
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
// 預定位 Entity
public class SeatReservation: Entity
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
		return new string[] {};
	}
	public static SeatReservation Create(string ReserveName)
	{
		return new SeatReservation() { Id = Guid.NewGuid() };
	}
}

public class Membership: Entity
{
}

public class Account: Entity, IAggregateRoot
{
}

public class ConcertVenue: ValueObject
{
	
}
#endregion

#region Application Services
namespace Application.ConcertTickets
{
	public interface IReserveRepository
	{
		int SaveConcertReservation(Ticket ticket);
	}
	
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
// DTO 組件
namespace Application.DTO
{
	public class ReserveDTO
	{
		public string ReserveID { get; set; }
		public DateTime? ReserveTime { get; set; }
		public int? ShowTime {get;set;}
	}
}
#endregion

#region Infrastructure
// 基礎建設
public class ReserveRepository : IReserveRepository
{
	//  預定購票作業
	public int SaveConcertReservation(Ticket ticket)
	{
		return ticket.SaveTicket();
	}
}
#endregion