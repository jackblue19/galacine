using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PriceDto
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public string SeatType { get; set; }
        public decimal PriceMultiplier { get; set; }
    }
}
