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
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;
        private readonly ICarDal _carDal;

        public BrandManager(IBrandDal brandDal, ICarDal carDal)
        {
            _brandDal = brandDal;
            _carDal = carDal;
        }

        //[SecuredOperations("admin")]
        [ValidationAspect(typeof(BrandValidation))]
        [CacheRemoveAspect("IBrandService.Get")]
        [TransactionScopeAspect]
        public IResult Add(Brand brand)
        {
            var result = BusinessRules.Run(
                CheckIfBrandAlreadyExistInDb(brand.BrandName)
                );

            if (!result.Success)
            {
                return new ErrorResult(result.Message); 
            }

            _brandDal.Add(brand);
            return new SuccessResult(Messages.SuccessAdded);

        }

        [CacheRemoveAspect("IBrandService.Get")]
        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        public IResult Delete(int brandId)
        {
            var result = BusinessRules.Run(
                CheckIfBrandExistForDelete(brandId),
                CheckIfBrandUsedOnAnyCar(brandId)
                );

            if(!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            var brandToDelete = _brandDal.Get(b => b.BrandId == brandId);
            _brandDal.Delete(brandToDelete);
            return new SuccessResult(Messages.SuccessDeleted);
        }

        [CacheAspect(20)]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),Messages.ItemsListed);
        }


        private IResult CheckIfBrandAlreadyExistInDb(string brandName)
        {
            var brandsInDb = _brandDal.GetAll().Where(b => b.BrandName.ToUpper(new CultureInfo("tr-TR"))
                                                           == brandName.ToUpper(new CultureInfo("tr-TR"))).ToList();

            if (brandsInDb.Count > 0)
            {
                return new ErrorResult(Messages.ItemExist);
            }

            return new SuccessResult();
        }

        private IResult CheckIfBrandExistForDelete(int brandId)
        {
            var brandsInDb = _brandDal.GetAll().Where(b => b.BrandId == brandId);

            if (!brandsInDb.Any())
            {
                return new ErrorResult(Messages.DataNotFound);
            }

            return new SuccessResult();
        }

        private IResult CheckIfBrandUsedOnAnyCar(int brandId)
        {
            var result = _carDal.GetAll(c => c.BrandId == brandId);

            if (result.Any())
            {
                return new ErrorResult(Messages.BrandOnUse);
            }

            return new SuccessResult();
        }

    }
}
