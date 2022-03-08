using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Entities.DTOs;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }


        //[SecuredOperations("admin")]
        [ValidationAspect(typeof(CarValidations))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            var result = BusinessRules.Run(
                CheckIfCarAlreadyExistInDb(car)
            );

            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }


            _carDal.Add(car);
            return new SuccessResult(Messages.SuccessAdded);

        }

        //[SecuredOperations("admin")]
        [ValidationAspect(typeof(CarValidations))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarService.Get")]
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

        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.SuccessDeleted);
        }

        [CacheAspect(30)]
        public IDataResult<List<CarDetailsDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailsDto>>(_carDal.GetCarDetails(),Messages.ItemsListed);
        }

        [CacheAspect(10)]
        public IDataResult<CarDetailsDto> GetCar(int carId)
        {
            var result = BusinessRules.Run(
                CheckIfCarExist(carId)
            );

            if (!result.Success)
            {
                return new ErrorDataResult<CarDetailsDto>(result.Message);
            }
            return new SuccessDataResult<CarDetailsDto>(_carDal.GetCarDetails().Find(c=> c.CarId == carId), Messages.ItemsListed);
        }


        public IDataResult<List<CarDetailsDto>> GetCarsByFilter(int brandId, int colorId)
        {
            if (brandId == 0)
            {
                return new SuccessDataResult<List<CarDetailsDto>>(_carDal.GetCarDetails().Where(c => c.ColorId == colorId).ToList());
            } 
            if (colorId == 0)
            {
                return new SuccessDataResult<List<CarDetailsDto>>(_carDal.GetCarDetails().Where(c => c.BrandId == brandId).ToList());
            }

            return new SuccessDataResult<List<CarDetailsDto>>(_carDal.GetCarDetails().Where(c=> c.BrandId == brandId && c.ColorId == colorId).ToList(), Messages.ItemsListed);
        }

        [CacheAspect(30)]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),Messages.ItemsListed);
        }

        [CacheAspect(10)]
        public IDataResult<List<CarDetailsDto>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<CarDetailsDto>>(_carDal.GetCarDetails().FindAll(c=> c.BrandId == id),Messages.ItemsListed);
        }

        [CacheAspect(10)]
        public IDataResult<List<CarDetailsDto>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<CarDetailsDto>>(_carDal.GetCarDetails().FindAll(c => c.ColorId == id),Messages.ItemsListed);
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

        private IResult CheckIfCarAlreadyExistInDb(Car car)
        {
            var result = _carDal.GetAll().Where(c => c.ColorId == car.CarId && 
                                                     c.BrandId == car.BrandId &&
                                                     c.ModelYear == car.ModelYear &&
                                                     c.DailyPrice == car.DailyPrice && 
                                                     c.Description.ToLower(new CultureInfo("tr-TR")) == car.Description.ToLower(new CultureInfo("tr-TR")));

            if (result != null && result.Count() > 0)
            {
                return new ErrorResult(Messages.ItemExist);
            }

            return new SuccessResult();
        }


    }
}
