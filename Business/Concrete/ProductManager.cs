            using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.SeriLog.Logger;
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
    {/*
      * 
       
      * burada cache aspect ve cacheremove aspectleri eklemeyi unutma
      

        satıştaki ürünlerimi getirmesi için get by seller Id
      *          
       */



        private readonly IProductDal _productDal;
        private readonly ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [ValidationAspect(typeof(ProductValidator))]
        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("IProductService.Get")]
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

        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("IProductService.Get")]
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

        [ValidationAspect(typeof(ProductValidator))]
        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("IProductService.Get")]
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

        [LogAspect(typeof(FileLogger))]
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        [LogAspect(typeof(FileLogger))]
        [CacheAspect]
        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == categoryId));
        }

        [LogAspect(typeof(FileLogger))]
        [CacheAspect]
        public IDataResult<List<Product>> GetAllByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.BrandId == brandId));
        }

        [LogAspect(typeof(FileLogger))]
        [CacheAspect]
        public IDataResult<List<ProductDetailDto>> GetAllProductsDetails()//tüm ürünlerin detaillerini listeler halinde getirir
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetAllProductsDetails());
        }

        [LogAspect(typeof(FileLogger))]
        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        [LogAspect(typeof(FileLogger))]
        [CacheAspect]
        public IDataResult<List<Product>> GetByUnitPrice(double min, double max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        [LogAspect(typeof(FileLogger))]
        [CacheAspect]
        public IDataResult<List<ProductDetailDto>> GetProductsBySellerId(int sellerId)
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductsBySellerId(sellerId));//bu metod şüpheli mutlaka testet
        }

        public IResult CheckOfferable(int productId)
        {
            var specimen = _productDal.Get(p => p.ProductId == productId);
            bool result = specimen.IsOfferable;
            ;

            return new Result(result);
        }

        [LogAspect(typeof(FileLogger))]
        [CacheAspect]
        public double GetProductPriceById(int productId)
        {
            var specimen = _productDal.Get(p => p.ProductId == productId);
            var result = specimen.UnitPrice;
            return  result;
        }

        [LogAspect(typeof(FileLogger))]
        [CacheAspect]
        public int GetUserIdByProductId(int productId)
        {
           var specimen = _productDal.Get(p => p.ProductId == productId);
            var result = specimen.SellerId;
            return result;
        }

        //-- kurallar


        private IResult CheckIfCategoryIdExist(int categoryId)//Id girdin den böyle bir id varmı
        {
            var result = _productDal.GetAll(p => p.CategoryId== categoryId).Any();
            if (result)//girilen category id ye eş bir category id database'de yoksa
            {
                return new ErrorResult(Messages.CategoryDoesntExist);
            }//varsa devamet
            return new SuccessResult();
        }
        //CheckIfColorIdExist
        private IResult CheckIfColorIdExist(int colorId)//Id girdin den böyle bir id varmı
        {
            var result = _productDal.GetAll(p => p.ColorId== colorId).Any();
            if (result)
            {
                return new ErrorResult(Messages.ColorDoesntExist);
            }
            return new SuccessResult();
        }

        
    }
}
