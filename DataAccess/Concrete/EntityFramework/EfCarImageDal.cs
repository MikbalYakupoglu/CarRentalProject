using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework;

public class EfCarImageDal : EfEntityRepositoryBase<CarImage,CarRentalContext> , ICarImageDal
{
    public List<CarImageDto> GetCarImages()
    {
        using (CarRentalContext context = new CarRentalContext())
        {
            var result = from car in context.Cars
                join carImage in context.CarImages
                    on car.CarId equals  carImage.CarId
                    into t from temp in t.DefaultIfEmpty()
                select new CarImageDto()
                {
                    CarId = car.CarId,
                    ImagePath = temp.ImagePath
                };

            return result.ToList();
        }
    }
}