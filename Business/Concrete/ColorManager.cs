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
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        readonly IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new DataResult<List<Color>>(_colorDal.GetAll(),true,Messages.ItemsListed);
        }

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

        public IResult Delete(Color color)
        {
            var colorsToDelete = _colorDal.GetAll().Where(c => c.ColorName == color.ColorName).ToList();

            if (colorsToDelete.Count == 0)
            {
                return new ErrorDataResult<Brand>(Messages.DataNotFound);
            }

            foreach (var colors in colorsToDelete)
                _colorDal.Delete(colors);

            return new SuccessDataResult<Brand>(Messages.SuccessDeleted);
        }
    }
}
