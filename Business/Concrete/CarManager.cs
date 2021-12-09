using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public void Add(Car car)
        {
            if (car.Description.Length > 2 && car.DailyPrice > 0)
            {
                _carDal.Add(car);
                Console.WriteLine("{0} - {1} --> {2}$ successfull added.",car.Id,car.DailyPrice,car.Description);
            }
            else
            {
                Console.WriteLine("Add Command Failed.");
            }
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public List<Car> GetCarsByBrandId(int id)
        {
            return _carDal.GetAll(c=> c.BrandId== id);
        }

        public List<Car> GetCarsByColorId(int id)
        {
            return _carDal.GetAll(c=> c.ColorId == id);
        }
    }
}
