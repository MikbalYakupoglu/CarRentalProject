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
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public void Add(Brand brand)
        {
            try
            {
                var brandsInDB = _brandDal.GetAll().Where(b => b.BrandName == brand.BrandName).ToList();

                if (brandsInDB.Count > 0)
                {
                    throw new Exception();
                }
                else if (brand.BrandName.Length < 2)
                {
                    Console.WriteLine("Geçersiz Marka İsmi (Min 2 harf olmalı)");
                }
                else
                {
                    _brandDal.Add(brand);
                    Console.WriteLine("{0} - {1} Successful Added.", brand.Id, brand.BrandName);
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Eleman Zaten Bulunuyor.");
            }
        }

        public void Delete(Brand brand)
        {
            try
            {
                var brandsToDelete = _brandDal.GetAll().Where(b => b.BrandName == brand.BrandName).ToList();

                if (brandsToDelete.Count == 0)
                {
                    throw new InvalidOperationException();
                }

                foreach (var brands in brandsToDelete)
                {
                        _brandDal.Delete(brands);
                        Console.WriteLine("{0} - {1} Successful Deleted.", brand.Id, brand.BrandName);
                }

            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Eleman Bulunamadı");
            }

        }

        public List<Brand> GetAll()
        {
            return _brandDal.GetAll();
        }

    }
}
