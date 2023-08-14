namespace Domain.ConcertTickets
{
    /// <summary>
    /// 演唱會場次
    /// </summary>
    public class ConcertVenue: ValueObject
    {
        public int ShowTimeNum { get; set; }
        public string ShowTimeName { get; set; }
    }
}