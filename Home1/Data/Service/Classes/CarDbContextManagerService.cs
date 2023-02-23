using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfApp4.Data.Dbcontexts;
using WpfApp4.Data.Models;
using WpfApp4.Data.Service.Interfaces;

namespace WpfApp4.Data.Service.Classes;
internal class CarDbContextManagerService : ICarDbContextManagerService
{
    public async void AddCar(CarModel model)
    {
        try
        {
            using (CarMarketDbContext dbContext = new())
            {
                await dbContext.CarModels.AddAsync(model);
                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public ICollection<CarModel>? GetListCar()
    {
        try
        {
            using (CarMarketDbContext dbContext = new())
            {
                var carModels = dbContext.CarModels.Include(x => x.CarBrand)
                                                   .Include(x => x.CarColor)
                                                   .ToList();
                return carModels as ICollection<CarModel>;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void SaveCarsDb(ICollection<CarModel> cars, ICollection<CarModel> deletedCarModels)
    {
        try
        {
            using (CarMarketDbContext dbContext = new())
            {
                foreach (var item in cars)
                {
                    dbContext.Entry(item).State = EntityState.Modified;
                }

                foreach (var item in deletedCarModels)
                {
                    dbContext.Entry(item).State = EntityState.Deleted;
                }

                dbContext.RemoveRange(deletedCarModels);
                dbContext.SaveChanges();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

}
