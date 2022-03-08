using System.Diagnostics;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;




namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpGet]
        public IActionResult ListAllImages()
        {
            var result = _carImageService.GetAllImages();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        
        [HttpPost]
        public IActionResult UploadImage([FromForm] IFormFile file ,[FromForm] CarImage carImage)
        {
            var fileExtension = Path.GetExtension(file.FileName);

            if (fileExtension == ".jpg" ||fileExtension == ".jpeg" || fileExtension == ".png")
            {
                var result = _carImageService.Add(file, carImage);

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return new UnsupportedMediaTypeResult();

        }
        
        
        [HttpPut]
        public IActionResult UpdateImage([FromForm] IFormFile file ,[FromForm] CarImage carImage)
        {
            
            var result = _carImageService.Update(file,carImage);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        
        [HttpDelete("{carImageId}")]
        public IActionResult DeleteImage(int carImageId)
        {
            var result = _carImageService.Delete(carImageId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("ListByCarId/{carId}")]
        public IActionResult ListByCarId(int carId)
        {
            var result = _carImageService.GetImagesByCarId(carId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetImageForExhibit")]
        public IActionResult GetImageForExhibit()
        {
            var result = _carImageService.GetImageForExhibit();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
