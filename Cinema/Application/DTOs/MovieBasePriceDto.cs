using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class MovieBasePriceDto
    {
        public int MovieId { get; set; }
        public decimal BasePrice { get; set; }
    }
}
