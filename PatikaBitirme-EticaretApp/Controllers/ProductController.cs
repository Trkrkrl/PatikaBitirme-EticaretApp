using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PatikaBitirme_EticaretApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {


            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbycategory")]
        public IActionResult GetByCategory(int categoryId)
        {
            var result = _productService.GetAllByCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("getallproductdetails")]
        public IActionResult GetProductDetails()
        {
            var result = _productService.GetAllProductsDetails();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [Authorize]

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [Authorize]

        [HttpPost("delete")]
        public IActionResult Delete(Product product)
        {
            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int sellerId = int.Parse(clm);
            product.SellerId = sellerId;
            var productOwnerUserId = _productService.GetUserIdByProductId(product.ProductId);   //bu kişi söz konusu ürünü yükleyen kişi mi

            if (productOwnerUserId == sellerId)
            {

                var result = _productService.Delete(product);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(Messages.UnAthorizedProductDeleteAttempt);

        }
        [Authorize]

        [HttpPost("update")]
        public IActionResult Update(Product product)
        {
            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int sellerId = int.Parse(clm);
            product.SellerId=sellerId;
            var productOwnerUserId = _productService.GetUserIdByProductId(product.ProductId);   //bu kişi söz konusu ürünü yükleyen kişi mi

            if (productOwnerUserId==sellerId)
            {
                var result = _productService.Delete(product);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(Messages.UnAthorizedProductUpdateAttempt);


        }

        //-
        [Authorize]

        [HttpGet("getmyproductsonsale")]
        public IActionResult GetByMyProductsOnSale()//satıcı kendi satmakta olduğu ürünlere erişebilir
        {
            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int sellerId = int.Parse(clm);
            var result = _productService.GetProductsBySellerId(sellerId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getproductsbysellerid")]//giriş yapmamış kullanıcılar belirli bir satıcının sattığı tüm ürünlere erişebilsinler
        public IActionResult GetBySellerId(int userId)
        {
            var result = _productService.GetProductsBySellerId(userId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
