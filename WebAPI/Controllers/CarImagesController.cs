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
            var fileType = file.FileName;

            var result = _carImageService.Add(file,carImage);

            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
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
        
        [HttpDelete]
        public IActionResult DeleteImage([FromForm] CarImage carImage)
        {
            var result = _carImageService.Delete(carImage);

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
