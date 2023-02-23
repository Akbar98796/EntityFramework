using System;
using System.Collections.Generic;

namespace WpfApp4.Data.Models;

public partial class CarBrandModel
{
    public int Id { get; set; }

    public string CarBrand { get; set; } = null!;

    public virtual ICollection<CarModel> CarModels { get; } = new List<CarModel>();
}
