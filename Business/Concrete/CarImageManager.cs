using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;


namespace Business.Concrete;

public class CarImageManager : ICarImageService
{
    private readonly ICarImageDal _carImageDal;
    private readonly IFileHelperService _fileHelperService;

    public CarImageManager(ICarImageDal carImageDal, IFileHelperService fileHelperService)
    {
        _carImageDal = carImageDal;
        _fileHelperService = fileHelperService;
    }

    [SecuredOperations("admin")]
    [TransactionScopeAspect]
    [CacheRemoveAspect("ICarImageService.Get")]
    public IResult Add(IFormFile file, CarImage carImage)
    {

        var result = BusinessRules.Run(
            CheckIfImageLimitExceed(carImage.CarId)
            );

        if (result != null)
        {
            return result;
        }
        var imagePathInApi = _fileHelperService.Upload(file);
        
        carImage.ImagePath = imagePathInApi;
        carImage.Date = DateTime.Now;
        _carImageDal.Add(carImage);
        
        return new SuccessResult(Messages.ImageAdded);
    }
    [SecuredOperations("admin")]
    [TransactionScopeAspect]
    [CacheRemoveAspect("ICarImageService.Get")]
    public IResult Update(IFormFile file, CarImage carImage)
    {
        var result = BusinessRules.Run(
            CheckIfImageLimitExceed(carImage.CarId)
        );

        if (result != null)
        {
            return result;
        }

        var oldImagePath = _carImageDal.GetAll().FirstOrDefault(cI => cI.CarId == carImage.CarId);
        var newImagePath = _fileHelperService.Update(file, oldImagePath.ImagePath);
        oldImagePath.ImagePath = newImagePath;
        _carImageDal.Update(oldImagePath);

        return new SuccessResult(Messages.SuccessUpdated);
    }

    [SecuredOperations("admin")]
    [TransactionScopeAspect]
    [CacheRemoveAspect("ICarImageService.Get")]
    public IResult Delete(CarImage carImage)
    {
        var oldCarImage = _carImageDal.GetAll().FirstOrDefault(cI => cI.CarId == carImage.CarId);
        if (oldCarImage == null)
        {
            return new ErrorResult(Messages.DataNotFound);
        }
        
        _fileHelperService.Delete(oldCarImage.ImagePath);
        _carImageDal.Delete(oldCarImage);
        return new SuccessResult(Messages.SuccessDeleted);
    }

    public IDataResult<List<CarImage>> GetAllImages()
    {
        return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(),Messages.ItemsListed);
    }

    public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
    {
        var result = BusinessRules.Run(
            CheckCarImage(carId)

            );

        if (!result.Success)
        {
            return new SuccessDataResult<List<CarImage>>(GetDefaultImage(carId).Data);
        }

        return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll().Where(c=> c.CarId== carId).ToList(),Messages.ItemsListed);
    }




    private IResult CheckIfImageLimitExceed(int carId)
    {
        var result = _carImageDal.GetAll().Where(cI => cI.CarId == carId).Count();

        if (result >= 5)
        {
            return new ErrorResult(Messages.CarImageCountExceed);
        }

        return new SuccessResult();
    }
    
    private IResult CheckCarImage(int carId)
    {
        var result = _carImageDal.GetAll().Count(c => c.CarId == carId);

        if (result  == 0)
        {
            return new ErrorResult();
        }
        return new SuccessResult();
    }
    
    private IDataResult<List<CarImage>> GetDefaultImage(int carId)
    {
           
        List<CarImage> carImage = new List<CarImage>();
        carImage.Add(new CarImage { CarId = carId, Date = DateTime.Now, ImagePath = "DefaultImage.png" });
        return new SuccessDataResult<List<CarImage>>(carImage);
    }
}