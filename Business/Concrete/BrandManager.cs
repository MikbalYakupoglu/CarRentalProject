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

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Add(Brand brand)
        {
            try
            {
                var brandsInDb = _brandDal.GetAll().Where(b => b.BrandName == brand.BrandName).ToList();

                if (brandsInDb.Count > 0)
                {
                    throw new Exception();
                }
                else if (brand.BrandName.Length < 2)
                {
                    return new ErrorDataResult<Brand>(Messages.ItemNameInValid);
                }
                else
                {
                    _brandDal.Add(brand);
                    return new SuccessDataResult<Brand>(Messages.SuccessAdded);
                }

            }
            catch (Exception)
            {
                return new ErrorDataResult<Brand>(Messages.ItemExist);
            }
        }

        public IResult Delete(Brand brand)
        {
            var brandsToDelete = _brandDal.GetAll().Where(b => b.BrandName == brand.BrandName).ToList();

                if (brandsToDelete.Count == 0)
                {
                    return new ErrorDataResult<Brand>(Messages.DataNotFound);
                }

                foreach (var brands in brandsToDelete)
                    _brandDal.Delete(brands);
                
                return new SuccessDataResult<Brand>(Messages.SuccessDeleted);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Brand>>(Messages.MaintenanceTime);
            }
            else
            {
                return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),Messages.ItemsListed);
            }
        }

    }
}
