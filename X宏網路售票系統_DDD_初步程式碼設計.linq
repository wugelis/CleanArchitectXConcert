<Query Kind="Program">
  <Namespace>Application.DTO</Namespace>
  <Namespace>Application.ConcertTickets</Namespace>
</Query>

void Main()
{
	DateTime? startShowTime = new System.DateTime(2023, 12, 1, 19, 30, 0);
	DateTime? endShowTime = new System.DateTime(2023, 12, 1, 21, 30, 0);
	
	// Controller 用法
	Application.ConcertTickets.ConcertTicketAppService app = new ConcertTicketAppService(new ReserveRepository());
	ReserveResponseDTO result = app.Reservation(
		new ReserveDTO()
		{
			ReserveID = "XXXXX10001", //訂位代號（流水號）
			ReserveName = "GelisWu", //定位大名
			ReserveTime = DateTime.Now, // 訂位時間、不等於票種時間 或 演唱會場時間 (the showtime for Concert Venue)
			ShowTime = new ShowTime(startShowTime, endShowTime)
		}
	);
	
	result.Dump();
	result.MySeatServe.ShowTime.GetEndShowTime().Dump();
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
// 票卷實體
public class Ticket: Entity, IAggregateRoot
{
	private SeatReservation _seatReservation;
	public SeatReservation SeatReservationInfo => _seatReservation;
	public Guid Id { get; set; }
	public string ReservatName { get; protected set;}
	// 確認購票
	public int SaveTicket()
	{
		return 0;
	}
	// 建立票卷實體
	public static Ticket Create(SeatReservation reserve)
	{
		return new Ticket() { Id = Guid.NewGuid(), _seatReservation = reserve };
	}
}
// 預定位 Entity
public class SeatReservation: Entity
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
		return new string[] {};
	}
	public static SeatReservation Create(string ReserveName, ShowTime ReserveShowTime)
	{
		return new SeatReservation() 
		{ 
			Id = Guid.NewGuid(), 
			ShowTime = ReserveShowTime,
			ReserveName = ReserveName
		};
	}
	//
	public void SetConcertVenue(ShowTime showTime)
	{
		
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

public class Membership: Entity
{
}

public class Account: Entity, IAggregateRoot
{
}
// 演唱會場次
public class ConcertVenue: ValueObject
{
	public int ShowTimeNum { get; set; }
	public string ShowTimeName {get;set;}

}
// 演出時間
[SerializableAttribute]
public class ShowTime: ValueObject
{
	public ShowTime(DateTime? startShowTime, DateTime? endShowTime) 
	{
		if(!startShowTime.HasValue || !endShowTime.HasValue)
		{
			throw new ShowTimeNotDefinedException(@"票種的開始時間 startShowTime 與 結束時間 endShowTime 不可為空白！");
		}
		_startShowTime = startShowTime;
		_endShowTime = endShowTime;
	}
	private DateTime? _startShowTime;
	public DateTime? GetStartShowTime()
	{
		return _startShowTime;
	}
	private DateTime? _endShowTime;
	public DateTime? GetEndShowTime()
	{
		return _endShowTime;
	}
}
// 票種場次未定義的自訂錯誤 Exception
public class ShowTimeNotDefinedException: Exception
{
	public ShowTimeNotDefinedException(string message) : base(message) {}
}
// 無效的訂位紀錄（可能該座位已經被預訂；或者是資料輸入有誤）
public class InvalidSeatReservationException: Exception
{
	public InvalidSeatReservationException(string message) : base(message) {}
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
		// 購票作業流程
		public ReserveResponseDTO Reservation(ReserveDTO ticketDto)
		{
			// 建立票種、若選擇時間有票種，進行購票作業
			SeatReservation seat = SeatReservation.Create(ticketDto.ReserveName, ticketDto.ShowTime);
			
			// 建立場次
			seat = seat.BuildConertVenue(seat);
			
			// 產生新的票卷（此預定保留 10 分鐘）
			Ticket ticket = Ticket.Create(seat);
			
			// 檢核購票時間、並檢核選擇票種是否還有位子？
			if(!ticket.SeatReservationInfo.CheckVenueIsExist())
			{
				throw new InvalidSeatReservationException("可能該座位已經被預訂, 或者是資料輸入有誤!");
			}
			
			// 預訂票卷（此預定預設保留 10 分鐘、即便執行了 SeatReservat() 但若未付款，10分鐘取消資格，將釋放此預訂票種給其他訂票作業的 Transaction）
			ticket = seat.SetReservat(ticket);
			//seat.Dump();
			//ticket.Dump();
			return new ReserveResponseDTO() {MySeatServe = seat, MyTicket = ticket };
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
		public string ReserveName {get;set;}
		public DateTime? ReserveTime { get; set; }
		public ShowTime ShowTime {get;set;}
	}
	public class ReserveResponseDTO
	{
		public SeatReservation MySeatServe { get; set;}
		public Ticket MyTicket {get;set;}
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