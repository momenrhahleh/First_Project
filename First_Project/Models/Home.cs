using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace First_Project.Models;

public partial class Home
{
    public decimal Homeid { get; set; }
    [NotMapped]

    public IFormFile? ImageFile { get; set; }
    public string? ImagePath { get; set; }

    //public byte[]? Image { get; set; }

    public string? Text { get; set; }

}
