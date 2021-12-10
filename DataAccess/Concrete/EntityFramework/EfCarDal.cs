using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car,CarInfoContext> , ICarDal
    {
        public List<CarDetailsDto> GetCarDetails()
        {
            using (CarInfoContext context = new CarInfoContext())
            {
                var result = from car in context.Cars
                    join color in context.Colors
                        on car.ColorId equals color.Id
                    join brand in context.Brands
                        on car.BrandId equals brand.Id
                    select new CarDetailsDto
                    {
                        CarId = car.Id,
                        BrandName = brand.BrandName,
                        ColorName = color.ColorName,
                        DailyPrice = car.DailyPrice
                    };
                    return result.ToList();
            }
        }
    }
}
