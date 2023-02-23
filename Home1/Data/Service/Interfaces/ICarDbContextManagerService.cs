using System.Collections.Generic;
using WpfApp4.Data.Models;

namespace WpfApp4.Data.Service.Interfaces;
internal interface ICarDbContextManagerService
{
    ICollection<CarModel>? GetListCar();
    void AddCar(CarModel model);
    void SaveCarsDb(ICollection<CarModel> cars, ICollection<CarModel> deletedCarModels);
}