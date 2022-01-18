using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Constants;
using Core.Results;
using Core.Utilities.Business;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IResult Add(Car car)
        { 
            if (car.Description.Length > 2 && car.DailyPrice > 0)
            {
                _carDal.Add(car);
                return new SuccessResult(Messages.SuccessAdded);
            }
            return new ErrorResult(Messages.ItemNameInValid);
            
        }

        public IResult Update(Car car)
        {
            // Null Exception
            var result = BusinessRules.Run(
                    CheckIfCarExist(car.CarId)
                );

                if (!result.Success)
                {
                    return result;
                }
            
            var carToUpdate = _carDal.GetAll().FirstOrDefault(c => c.CarId == car.CarId);
            
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.Description = car.Description;
                       
           _carDal.Update(carToUpdate);

            return new SuccessResult(Messages.SuccessUpdated);
        }

        public IResult Delete(Car car)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<CarDetailsDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailsDto>>(_carDal.GetCarDetails(),Messages.ItemsListed);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),Messages.ItemsListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c=> c.BrandId== id),Messages.ItemsListed);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new DataResult<List<Car>>(_carDal.GetAll(c=> c.ColorId == id),true,Messages.ItemsListed);
        }



        private IResult CheckIfCarExist(int id)
        {
            var result = _carDal.GetAll().Where(c => c.CarId == id).ToList();

            if (result.Count == 0)
            {
                return new ErrorResult(Messages.DataNotFound);
            }

            return new SuccessResult();
        }


    }
}
