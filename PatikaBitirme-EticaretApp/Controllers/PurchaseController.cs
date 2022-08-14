using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatikaBitirme_EticaretApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()//admin tarafından yapılan tüm purchase leri getirir
        {
            var result = _purchaseService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbycustomeruserid")]
        public IActionResult GetByCustomerId(int userId)//bu user idler claimsten gelecek
        {
            var result = _purchaseService.GetByCustomerUserId(userId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbypurchaseid")]
        public IActionResult GetByPurchaseId(int purchaseId)
        {
            var result = _purchaseService.GetByDetailsByPurchaseId(purchaseId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //post-add,update ,delete
        [HttpPost("Buy")]
        public IActionResult Buy(Purchase purchase)
        {
            var result = _purchaseService.Add(purchase);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }



        [HttpPost("cancel")]
        public IActionResult CancelPurchase(Purchase purchase)
        {
            var result = _purchaseService.CancelPurchase(purchase);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
    }
}
