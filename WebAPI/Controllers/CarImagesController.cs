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

        [HttpGet("ListAll")]
        public IActionResult ListAll()
        {
            var result = _carImageService.ListAllImages();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        
        [HttpPost("Upload")]
        public IActionResult UploadImage([FromForm] IFormFile file ,[FromForm] CarImage carImage)
        {
            var result = _carImageService.Add(file,carImage);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        
        [HttpPost("Update")]
        public IActionResult UpdateImage([FromForm] IFormFile file ,[FromForm] CarImage carImage)
        {
            return BadRequest();

            
            var result = _carImageService.Update(file,carImage);

            if (result.Success)
            {
                return Ok(result);
            }

        }
        
        [HttpPost("Delete")]
        public IActionResult DeleteImage([FromForm] CarImage carImage)
        {
            var result = _carImageService.Delete(carImage);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("ListByCarId")]
        public IActionResult ListByCarId(int id)
        {
            var result = _carImageService.ListImagesByCarId(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
    }
}
