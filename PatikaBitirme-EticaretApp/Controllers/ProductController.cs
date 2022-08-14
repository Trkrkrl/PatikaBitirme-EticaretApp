using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        //buna gerek varmı- çözüm üretemedim
        /* [HttpGet("gettheproductsdetail")]
         public IActionResult GetProductDetails(int productId)
         {
             var result = _productService.GetProductDetails(productId);
             if (result.Success)
             {
                 return Ok(result);
             }

             return BadRequest(result);
         }*/
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
        //-
        [HttpGet("getbysellerid")]
        public IActionResult GetBySellerId(int userId)
        {
            //burada userin seller id bilgisine erişmeliyim-burası da authorized olmalı--veya user girer direkt-veya claimsten alırım
            var result = _productService.GetProductsBySellerId(userId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
