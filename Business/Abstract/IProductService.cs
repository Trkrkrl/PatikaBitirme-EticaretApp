using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryId(int categoryId);
        IDataResult<List<Product>> GetByUnitPrice(double min, double max);
       // IDataResult<List<ProductDetailDto>> GetProductDetails(int productId);
        IDataResult<List<ProductDetailDto>> GetAllProductsDetails();
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product);
        IResult Delete(Product product);
        IResult Update(Product product);
        IDataResult<List<ProductDetailDto>> GetProductsBySellerId(int sellerId);
        IResult CheckOfferable(int productId);
        double GetProductPriceById(int productId);
        int GetUserIdByProductId(int productId);
        IDataResult<List<Product>> GetAllByBrandId(int brandId);
    }
}
