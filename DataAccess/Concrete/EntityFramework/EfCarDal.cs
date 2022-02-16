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
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalContext>, ICarDal
    {
        public List<CarDetailsDto> GetCarDetails()
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from car in context.Cars
                    join color in context.Colors
                        on car.ColorId equals color.ColorId
                    join brand in context.Brands
                        on car.BrandId equals brand.BrandId
                    //join carImages in context.CarImages
                    //    on car.CarId equals carImages.CarId
                    select new CarDetailsDto
                    {
                        CarId = car.CarId,
                        //ImagePath = carImages.ImagePath,
                        BrandId = brand.BrandId,
                        BrandName = brand.BrandName,
                        ColorId = color.ColorId,
                        ColorName = color.ColorName,
                        ModelYear = car.ModelYear,
                        DailyPrice = car.DailyPrice,
                        Description = car.Description
                    };
                return result.ToList();
            }
        }
    }
}
