using System;
using System.Collections.Generic;

namespace First_Project.Models;

public partial class Beneficiary
{
    public decimal Beneficiaryid { get; set; }

    public decimal? Userid { get; set; }

    public decimal? Subscriptionid { get; set; }

    public string? Name { get; set; }

    public string? Relationship { get; set; }
}
