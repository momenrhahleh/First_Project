using System;
using System.Collections.Generic;

namespace First_Project.Models;

public partial class Contactu
{
    public decimal Contactusid { get; set; }

    public string location { get; set; } = null!;

    public string PHONE { get; set; } = null!;

    public string? Email { get; set; }

}
