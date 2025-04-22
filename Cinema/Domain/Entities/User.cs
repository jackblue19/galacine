using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

//public partial class User
//{
//    public int UserId { get; set; }

//    public string Username { get; set; } = null!;

//    public string Password { get; set; } = null!;

//    public string? FirstName { get; set; }

//    public string? LastName { get; set; }

//    public string? Phone { get; set; }

//    public string Email { get; set; } = null!;

//    public string? Gender { get; set; }

//    public string? VerificationCode { get; set; }

//    public bool? AccStatus { get; set; }

//    public int RoleId { get; set; }

//    public DateTime? CreatedAt { get; set; }

//    public DateTime? UpdatedAt { get; set; }

//    public DateOnly? DateOfBirth { get; set; }

//    public string? Address { get; set; }

//    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

//    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

//    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

//    public virtual Role Role { get; set; } = null!;

//    public virtual ICollection<Service> ServiceApprovedByNavigations { get; set; } = new List<Service>();

//    public virtual ICollection<Service> ServiceCreatedByNavigations { get; set; } = new List<Service>();

//    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
//}
public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [StringLength(10)]
    [RegularExpression("Male|Female", ErrorMessage = "Giới tính chỉ được là 'Male' hoặc 'Female'.")]
    public string? Gender { get; set; }

    [StringLength(100)]
    public string? VerificationCode { get; set; }

    public bool? AccStatus { get; set; } = false;

    [ForeignKey(nameof(Role))]
    public int RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Service> ServiceApprovedByNavigations { get; set; } = new List<Service>();

    public virtual ICollection<Service> ServiceCreatedByNavigations { get; set; } = new List<Service>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

