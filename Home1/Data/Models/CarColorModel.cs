using System;
using System.Collections.Generic;

namespace WpfApp4.Data.Models;

public partial class CarColorModel
{
    public int Id { get; set; }

    public  string Color { get; set; } = null!;

    public virtual ICollection<CarModel> CarModels { get; } = new List<CarModel>();
}