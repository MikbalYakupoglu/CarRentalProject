using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _brandService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        

        [HttpPost]
        public IActionResult Add(Brand brand)
        {
            var result = _brandService.Add(brand);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpDelete]
        public IActionResult Delete(Brand brand)
        {
            var result = _brandService.Delete(brand);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
