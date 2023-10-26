using System;
using System.Collections.Generic;

namespace First_Project.Models;

public partial class Subscription
{
    public decimal Subscriptionid { get; set; }

    public decimal? Userid { get; set; }

    public DateTime? Subscriptiondate { get; set; }

    public decimal Amount { get; set; }

    public string? Paymentstatus { get; set; }

    public virtual Useradmin? User { get; set; }
}
