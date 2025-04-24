using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }
        public string ServiceDesc { get; set; } = null!;
        public int CreatedBy { get; set; }
        public int? ApprovedBy { get; set; }
        public bool IsApproved { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateServiceDto
    {
        [Required]
        public string ServiceDesc { get; set; } = null!;

        [Required]
        public int CreatedBy { get; set; }

        public string? Note { get; set; }
    }

    public class UpdateServiceDto
    {
        [Required]
        public string ServiceDesc { get; set; } = null!;

        public string? Note { get; set; }
    }
}

