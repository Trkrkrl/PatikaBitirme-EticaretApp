using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatikaBitirme_EticaretApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = "Image")] IFormFile file, [FromForm] ProductImage productImage)
        {
            var result = _productImageService.Add(file, productImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        //---
        [HttpPost("delete")]
        public IActionResult Delete(ProductImage productImage)
        {
            var productDeleteImage = _productImageService.GetByProductImageId(productImage.ProductId).Data;
            var result = _productImageService.Delete(productDeleteImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        //--
        [HttpPost("update")]
        public IActionResult Update([FromForm] IFormFile file, [FromForm] ProductImage productImage)
        {
            var result = _productImageService.Update(file, productImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

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
        public IActionResult GetByCarId(int productId)
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
