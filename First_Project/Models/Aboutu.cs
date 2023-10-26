using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace First_Project.Models;

public partial class Aboutu
{
    public decimal Aboutusid { get; set; }

    [NotMapped]

    public IFormFile? ImageFile { get; set; }
    public string? ImagePath { get; set; }

    //public byte[]? Image { get; set; }

    public string? Paragraphtext { get; set; }

    public string? Text { get; set; }

}
