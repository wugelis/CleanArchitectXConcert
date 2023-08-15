using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ConcertTickets
{
    /// <summary>
    /// web Adapter api 請求的資料傳輸物件
    /// </summary>
    public class ReserveRequestDTO
    {
        private string _startShowTime;
        private string _endShowTime;

        public string ReserveName { get; set; }
        public DateTime? startShowTime
        {
            get => DateTime.Parse(_startShowTime);
            set => _startShowTime = value!.Value.ToString("yyyy/MM/dd HH:mm:ss");
        }
        public DateTime? endShowTime 
        {
            get => DateTime.Parse(_endShowTime);
            set => _endShowTime = value!.Value.ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}
