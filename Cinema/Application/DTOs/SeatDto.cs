using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SeatDto
    {
        public int SeatId { get; set; }
        public int RoomId { get; set; }
        public int RowNo { get; set; }
        public int ColNo { get; set; }
        public string SeatType { get; set; }
        public string SeatStatus { get; set; }
    }
}
