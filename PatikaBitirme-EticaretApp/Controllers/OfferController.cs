using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatikaBitirme_EticaretApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        //burada ek düzenlemeler yapılarak bilgiler claimslerden alınacak
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }
        [HttpPost("makeoffer")]
        public IActionResult MakeOffer(Offer offer)//add
        {
            var result = _offerService.Add(offer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("canceloffer")]
        public IActionResult CancelOffer(Offer offer)
        {
            var result = _offerService.Delete(offer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("updateoffer")]
        public IActionResult Update(Offer offer)
        {
            var result = _offerService.Update(offer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("getbyofferid")]
        public IActionResult GetByOfferId(int offerId)
        {
            var result = _offerService.GetByOfferId(offerId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getsentbycustomerid")]
        public IActionResult GetReceivedByCustomerId(int customerUserId)
        {
            var result = _offerService.GetSentByCustomerId(customerUserId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _offerService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpGet("getofferdetailbyofferid")]
        public IActionResult GetOfferDetailByOfferId(int offerId)
        {
            var result = _offerService.GetByOfferId(offerId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpPost("acceptoffer")]
        public IActionResult AcceptOffer(Offer offer)
        {
            var result = _offerService.AcceptOffer(offer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }



    }
}
