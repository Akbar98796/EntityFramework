using System;
using System.Collections.Generic;
using WpfApp4.Data.Models;

namespace WpfApp4.Data.Models;

public partial class CarModel
{
    public int Id { get; set; }

    public int Year { get; set; }

    public string Model { get; set; } = null!;

    public int CarBrandId { get; set; }

    public int CarColorId { get; set; }

    public string? Description { get; set; }

    public virtual CarBrandModel CarBrand { get; set; } = new();

    public virtual CarColorModel CarColor { get; set; } = new();
}
