using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ConcertTickets
{
    /// <summary>
    /// 場次值物件
    /// </summary>
    public class ShowTime : ValueObject
    {
        public ShowTime(DateTime? startShowTime, DateTime? endShowTime)
        {
            if (!startShowTime.HasValue || !endShowTime.HasValue)
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
}
