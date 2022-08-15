using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize]
        [HttpPost("makeoffer")]
        public IActionResult MakeOffer(Offer offer)//add
        {
            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int senderId = int.Parse(clm);

            offer.SenderUserId = senderId;

            var result = _offerService.Add(offer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpPost("canceloffer")]
        public IActionResult CancelOffer(Offer offer)
        {
            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int senderId = int.Parse(clm);

            offer.SenderUserId = senderId;

            var result = _offerService.Delete(offer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [Authorize]
        [HttpPost("updateoffer")]
        public IActionResult Update(Offer offer)
        {
            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int senderId = int.Parse(clm);

            offer.SenderUserId = senderId;

            var result = _offerService.Update(offer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("getbyofferid")]
        public IActionResult GetByOfferId(int offerId)
        {
            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int userId = int.Parse(clm);

            

            var result = _offerService.GetByOfferId(offerId);
            if (result.Success&&result.Data.SenderUserId==userId|| result.Success && result.Data.ReceiverUserId == userId)
            {
                
                return Ok(result);
            }
            return BadRequest(Messages.YouAreNotAllowedToViewThisOffer);
        }

        [Authorize]
        [HttpGet("getsentbycustomerid")]
        public IActionResult GetReceivedByCustomerId()//otomatik claimsten alır
        {
            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int userId = int.Parse(clm);
            

            var result = _offerService.GetSentByCustomerId(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [Authorize]//admin gerekecek
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
        [Authorize]
        [HttpGet("getofferdetailbyofferid")]
        public IActionResult GetOfferDetailByOfferId(int offerId)
        {
            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int userId = int.Parse(clm);

            var result = _offerService.GetByOfferId(offerId);
            if (result.Success && result.Data.SenderUserId == userId || result.Success && result.Data.ReceiverUserId == userId)
            {

                return Ok(result);
            }
            return BadRequest(Messages.YouAreNotAllowedToViewThisOfferDetails);
            

        }

        [Authorize]
        [HttpPost("acceptoffer")]
        public IActionResult AcceptOffer(Offer offer)
        {

            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int userId = int.Parse(clm);
            if (userId==offer.ReceiverUserId)
            {
                var result = _offerService.AcceptOffer(offer);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);

            }
            return BadRequest(Messages.NotAllowedToAcceptThisOffer);
        }
        [Authorize]
        [HttpPost("declineoffer")]
        public IActionResult DeclineOffer(Offer offer)
        {

            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int userId = int.Parse(clm);
            if (userId == offer.ReceiverUserId)
            {
                var result = _offerService.DeclineOffer(offer);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);

            }
            return BadRequest(Messages.NotAllowedToAcceptThisOffer);
        }



    }
}
