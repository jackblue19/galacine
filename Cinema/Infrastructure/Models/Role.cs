using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleDesc { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
