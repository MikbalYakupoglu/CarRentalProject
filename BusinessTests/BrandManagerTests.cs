using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Moq;
using Xunit;

namespace BusinessTests
{
    public class BrandManagerTests
    {
        private void saveChanges()
        {
            _mockedBrandDal.Setup(m => m.GetAll(null)).Returns(_dbBrands);
        }

        private Mock<IBrandDal> _mockedBrandDal;
        private List<Brand> _dbBrands;
        public BrandManagerTests()
        {
            _mockedBrandDal = new Mock<IBrandDal>();

            _dbBrands = new List<Brand>
            {
                new() {BrandId = 1, BrandName = "Mercedes"},
                new() {BrandId = 2, BrandName = "Audi"},
                new() {BrandId = 3, BrandName = "BMW"}
            };
            
            saveChanges();
        }

        [Fact]
        public void GetAll_MustGetAllBrands()
        {
            IBrandService brandService = new BrandManager(_mockedBrandDal.Object);
            List<Brand> brands = brandService.GetAll().Data;
            Assert.Equal(_dbBrands,brands);
        }

        [Fact]
        public void Add_MustAddBrandToDb()
        {

            IBrandService brandService = new BrandManager(_mockedBrandDal.Object);
            brandService.Add(new Brand
            {
                BrandId = 1001,
                BrandName = "Togg"
            });
            List<Brand> brands = brandService.GetAll().Data;
            saveChanges();

            Assert.NotEqual(_dbBrands, brands);
        }

        [Fact]
        public void Delete_MustDeleteBrandFromDb()
        {
            IBrandService brandService = new BrandManager(_mockedBrandDal.Object);
            List<Brand> brands = brandService.GetAll().Data;




            Assert.NotEqual(_dbBrands, brands);
        }
    }
}