using Business.Abstract;
using Business.Constants;
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

    
    public IResult Add(IFormFile file, CarImage carImage)
    {

        var result = BusinessRules.Run(
            CheckIfImageCountExceed(carImage.CarId)
            );

        if (result != null)
        {
            return result;
        }
        
        carImage.ImagePath = _fileHelperService.Upload(file);
        carImage.Date = DateTime.Now;
        _carImageDal.Add(carImage);
        
        return new SuccessResult(Messages.ImageAdded);
    }

    
    public IResult Update(IFormFile file, CarImage carImage)
    {
        throw new NotImplementedException();
    }

    public IResult Delete(CarImage carImage)
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<CarImage>> ListAllImages()
    {
        return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(),Messages.ItemsListed);
    }

    public IDataResult<List<CarImage>> ListImagesByCarId(int carId)
    {
        throw new NotImplementedException();
    }

    public IDataResult<CarImage> ListImageByImageId(int imageId)
    {
        throw new NotImplementedException();
    }





    private IResult CheckIfImageCountExceed(int carId)
    {
        var result = _carImageDal.GetAll().Where(cI => cI.CarId == carId).Count();

        if (result >= 5)
        {
            return new ErrorResult(Messages.CarImageCountExceed);
        }

        return new SuccessResult();
    }
}