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

        [HttpGet("{carId}")]
        public IActionResult GetCarById(int carId)
        {
            var result = _carService.GetCar(carId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _carService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetCarsByBrand/{brandId}")]
        public IActionResult GetCarsByBrandId(int brandId)
        {
            var result = _carService.GetCarsByBrandId(brandId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetCarsByColor/{colorId}")]
        public IActionResult GetCarsByColorId(int colorId)
        {
            var result = _carService.GetCarsByColorId(colorId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetCarsByFilter")]
        public IActionResult GetCarsByFilter(int brandId, int colorId)
        {
            var result = _carService.GetCarsByFilter(brandId, colorId);

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

        [HttpPost]
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult Update(Car car)
        {
            var result = _carService.Update(car);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
