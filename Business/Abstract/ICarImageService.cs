using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract;

public interface ICarImageService
{
    IResult Add(IFormFile file, CarImage carImage);
    IResult Update(IFormFile file, CarImage carImage);
    IResult Delete(int carImageId);
    IDataResult <List<CarImageDto>> GetImageForExhibit();
    IDataResult<List<CarImage>> GetAllImages();
    IDataResult<List<CarImage>> GetImagesByCarId(int carId);

}