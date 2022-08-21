using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _addressService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data); ;

            }
            return BadRequest(result);
        }

        [HttpGet("GetByUserId")]
        public IActionResult GetById(int userId)
        {
            
            if (userId == 0)
            {
                var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
                userId = int.Parse(clm);
            }

            var result = _addressService.GetByUserId(userId);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);

        }
        [HttpPost("Add")]
        public IActionResult Add(Address address)
        {
            if (address.UserId==0)
            {
                var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
                address.UserId = int.Parse(clm);
            }
           

            var result = _addressService.Add(address);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);

        }
        [HttpPost("Update")]
        public IActionResult Update(Address address)
        {
            if (address.UserId == 0)
            {
                var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
                address.UserId = int.Parse(clm);
            }
            var result = _addressService.Update(address);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);

        }
        [HttpPost("Delete")]
        public IActionResult Delete(Address address)
        {
            var result = _addressService.Delete(address);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);

        }
    }
}
