using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace First_Project.Models;

public partial class Useradmin
{
    public decimal Userid { get; set; }

    //public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public decimal? Userrole { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public string? ImagePath { get; set; }

    //public byte[]? Image { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual Role? UserroleNavigation { get; set; }
    //public decimal? RoleId { get; set; }
}
