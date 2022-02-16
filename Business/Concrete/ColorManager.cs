using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
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
        readonly IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }


        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Add(Color color)
        {
            if (_colorDal.GetAll().Any(c=> c.ColorName == color.ColorName))
            {
                return new ErrorResult(Messages.ItemExist);
            }
            else
            {
               _colorDal.Add(color);
               return new SuccessResult(Messages.SuccessAdded);
            }
        }

        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(Color color)
        {
            var result = BusinessRules.Run(
                CheckIfColorExist(color.ColorName)
            );

            if (!result.Success)
            {
                return result;
            }
            
            var colorsToDelete = _colorDal.GetAll().Where(c => c.ColorName == color.ColorName).ToList();
            
            foreach (var colors in colorsToDelete)
                _colorDal.Delete(colors);

            return new SuccessDataResult<Brand>(Messages.SuccessDeleted);
        }

        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(Color color)
        {
            var result = BusinessRules.Run(
                CheckIfColorExist(color.ColorName)
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

        private IResult CheckIfColorExist(string colorName)
        {
            var result = _colorDal.GetAll().Where(c => c.ColorName == colorName).ToList();
            if (result.Count == 0)
            {
                return new ErrorResult(Messages.DataNotFound);
            }

            return new SuccessResult();
        }
    }
}
