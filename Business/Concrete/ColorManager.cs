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
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Business;
using Microsoft.EntityFrameworkCore;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;
        private readonly ICarDal _carDal;

        public ColorManager(IColorDal colorDal, ICarDal carDal)
        {
            _colorDal = colorDal;
            _carDal = carDal;
        }


        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Add(Color color)
        {
            var result = BusinessRules.Run(
                CheckIfSameColorExistInDb(color)
                );

            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }
            
            _colorDal.Add(color);
            return new SuccessResult(Messages.SuccessAdded);
            
        }


        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(int colorId)
        {
            var result = BusinessRules.Run(
                CheckIfColorExistForDelete(colorId),
                CheckIfColorUsedOnAnyCar(colorId)
            );

            if (!result.Success)
            {
                return result;
            }
            
            var colorsToDelete = _colorDal.GetAll().Where(c => c.ColorId == colorId).ToList();
            
            foreach (var colors in colorsToDelete)
                _colorDal.Delete(colors);

            return new SuccessResult(Messages.SuccessDeleted);
        }

        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(Color color)
        {
            var result = BusinessRules.Run(
                CheckIfColorExistForDelete(color.ColorId)
            );

            if (!result.Success)
            {
                return result;
            }
            
            var colorToUpdate = _colorDal.GetAll().FirstOrDefault(c => c.ColorName == color.ColorName);

            colorToUpdate.ColorName = color.ColorName;
            _colorDal.Update(colorToUpdate);

            return new SuccessResult(Messages.SuccessUpdated);
        }


        public IDataResult<List<Color>> GetAll()
        {
            return new DataResult<List<Color>>(_colorDal.GetAll(), true, Messages.ItemsListed);
        }

        private IResult CheckIfColorExistForDelete(int colorId)
        {
            var result = _colorDal.GetAll().Where(c => c.ColorId == colorId).ToList();
            if (result.Count == 0)
            {
                return new ErrorResult(Messages.DataNotFound);
            }

            return new SuccessResult();
        }

        private IResult CheckIfColorUsedOnAnyCar(int colorId)
        {
            var result = _carDal.GetAll(c => c.ColorId == colorId);

            if (result.Any())
            {
                return new ErrorResult(Messages.ColorOnUse);
            }

            return new SuccessResult();
        }

        private IResult CheckIfSameColorExistInDb(Color color)
        {
            var result = _colorDal.GetAll(c => c.ColorName.ToLower() == color.ColorName.ToLower());

            if (result.Any())
            {
                return new ErrorResult(Messages.ItemExist);
            }
            return new SuccessResult();
        }

    }
}
