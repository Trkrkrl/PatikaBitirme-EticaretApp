using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _productImageService;
        private readonly IProductService _productService;
        public ProductImageController(IProductImageService productImageService,IProductService productService)
        {
            _productImageService = productImageService;
            _productService = productService;
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = "Image")] IFormFile file, [FromForm] ProductImage productImage)
        {
            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int userId = int.Parse(clm);
            productImage.SellerUserId = userId;//şu an giriş yapılı kişinin userid'sini  atadık
            var productOwnerUserId = _productService.GetUserIdByProductId(productImage.ProductId);   //bu kişi söz konusu ürünü yükleyen kişi mi
            if (productOwnerUserId==userId)
            {
                var result = _productImageService.Add(file, productImage);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(Messages.UnAthorizedAttempt2);
        }
        //---
        [Authorize]
        [HttpPost("delete")]
        public IActionResult Delete(ProductImage productImage)
        {
            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int userId = int.Parse(clm);
            productImage.SellerUserId = userId;//şu an giriş yapılı kişinin userid'sini  atadık
            var productOwnerUserId = _productService.GetUserIdByProductId(productImage.ProductId);   //bu kişi söz konusu ürünü yükleyen kişi mi
            if (productOwnerUserId == userId)
            {
                var productDeleteImage = _productImageService.GetByProductImageId(productImage.ProductId).Data;
                var result = _productImageService.Delete(productDeleteImage);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(Messages.UnAthorizedProductImageDeleteAttempt);
            
        }
        //--
        [Authorize]
        [HttpPost("update")]
        public IActionResult Update([FromForm] IFormFile file, [FromForm] ProductImage productImage)
        {
            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int userId = int.Parse(clm);
            productImage.SellerUserId = userId;//şu an giriş yapılı kişinin userid'sini  atadık
            var productOwnerUserId = _productService.GetUserIdByProductId(productImage.ProductId);   //bu kişi söz konusu ürünü yükleyen kişi mi
            if (productOwnerUserId == userId)
            {
                var result = _productImageService.Update(file, productImage);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(Messages.UnAthorizedProductImageUpdateAttempt);
           

        }

        //---------------------------------------------------------------
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _productImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        //--
        [HttpGet("getbyproductid")]
        public IActionResult GetByProductId(int productId)
        {
            var result = _productImageService.GetByProductId(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return Ok(result);
        }
        //--
        
        [HttpGet("getbyproductimageid")]
        public IActionResult GetByImageId(int productImageId)
        {
            var result = _productImageService.GetByProductImageId(productImageId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
