using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<CarDetailsDto>> GetCarsByBrandId(int id);
        IDataResult<List<CarDetailsDto>> GetCarsByColorId(int id);
        IDataResult<List<CarDetailsDto>> GetCarsByFilter(int brandId, int colorId);
        IDataResult<List<Car>> GetAll();
        IDataResult<List<CarDetailsDto>> GetCarDetails();
        IDataResult<CarDetailsDto> GetCar(int carId);
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        
    }
}
