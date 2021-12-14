using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Concrete;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ICarService _carService;
        
        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _carService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetCarsByBrand")]
        public IActionResult GetCarsByBrandId(int id)
        {
            var result = _carService.GetCarsByBrandId(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetCarsByColor")]
        public IActionResult GetCarsByColorId(int id)
        {
            var result = _carService.GetCarsByColorId(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetCarDetails")]
        public IActionResult GetCarDetails()
        {
            var result = _carService.GetCarDetails();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


    }
}
