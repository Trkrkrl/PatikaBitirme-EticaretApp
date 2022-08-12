using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {/*satıcı ürün ekleyebilir
      * bunun için satıcı mı kontrolü- 
      * 
      * 
      * müşteri 
      *     getall products
      *     get by category id
      * get by seller id-bu da satıcı idsine göre
      * 
      * burada cache aspect ve cacheremove aspectleri eklemeyi unutma
      *     
      *          CheckIfProductNameIsValid(product.ProductName),=>bunlari validaton ile yapacağız
                CheckIfProductDescriptionIsValid(product.Description),
                CheckIfCategoryEntryIsValid(product.CategoryId), sadece bir tane id bi girildi
                CheckIfColorEntryIsValid(product.ColorId)


      *          
       */



        private readonly IProductDal _productDal;
        private readonly ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(
           
                CheckIfCategoryIdExist(product.CategoryId),
                CheckIfColorIdExist(product.ColorId)
                );

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            IResult result = BusinessRules.Run(

                CheckIfCategoryIdExist(product.CategoryId),
                CheckIfColorIdExist(product.ColorId)
                );

            if (result != null)
            {
                return result;
            }
            _productDal.Delete(product);
            return new Result(true, Messages.ProductDeleted);
        }
        public IResult Update(Product product)
        {
            IResult result = BusinessRules.Run(

                CheckIfCategoryIdExist(product.CategoryId),
                CheckIfColorIdExist(product.ColorId)
                );

            if (result != null)
            {
                return result;
            }
            _productDal.Update(product);
            return new Result(true, Messages.ProductUpdated);
        }
        //--

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == categoryId));
        }

        public IDataResult<List<ProductDetailDto>> GetAllProductsDetails()//tüm ürünlerin detaillerini listeler halinde getirir
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetAllProductsDetails());
        }
        

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        //-- kurallar

        
        private IResult CheckIfCategoryIdExist(int categoryId)//Id girdin den böyle bir id varmı
        {
            var result = _productDal.GetAll(p => p.CategoryId== categoryId).Any();
            if (!result)//girilen category id ye eş bir category id database'de yoksa
            {
                return new ErrorResult(Messages.CategoryDoesntExist);
            }//varsa devamet
            return new SuccessResult();
        }
        //CheckIfColorIdExist
        private IResult CheckIfColorIdExist(int colorId)//Id girdin den böyle bir id varmı
        {
            var result = _productDal.GetAll(p => p.ColorId== colorId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.ColorDoesntExist);
            }
            return new SuccessResult();
        }



    }
}
