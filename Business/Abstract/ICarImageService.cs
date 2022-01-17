using Core.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract;

public interface ICarImageService
{
    IResult Add(IFormFile file, CarImage carImage);
    IResult Update(IFormFile file, CarImage carImage);
    IResult Delete(CarImage carImage);

    IDataResult<List<CarImage>> ListAllImages();
    IDataResult<List<CarImage>> ListImagesByCarId(int carId);
    IDataResult<CarImage> ListImageByImageId(int imageId);
    
}