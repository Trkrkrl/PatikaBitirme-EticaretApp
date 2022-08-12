using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductImageManager : IProductImageService
    {
        //burada add- update ve diğer gereken kısımlara ya validator koyacaz- yada kural methoduyla görse boyutunu kontrol ettireccez
        //yine de ödev dosasını tekrar  oku
        private readonly IProductImageDal _productImageDal;
        private readonly IFileHelper _fileHelper;


        public ProductImageManager(IProductImageDal productImageDal, IFileHelper fileHelper)
        {
            _productImageDal = productImageDal;
            _fileHelper = fileHelper;
        }

        public IResult Add(IFormFile file, ProductImage productImage)
        {

            IResult result = BusinessRules.Run(CheckIfProductImageLimit(productImage.ProductId));//limit kontrolu-asagida yazildi
            if (result != null)
            {
                return result;
            }
            productImage.ImagePath = _fileHelper.Upload(file, PathConstants.ImagesPath);
            productImage.Date = DateTime.Now;
            _productImageDal.Add(productImage);
            return new SuccessResult("Resim başarıyla yüklendi");
        }

        public IResult Delete(ProductImage productImage)
        {
            _fileHelper.Delete(PathConstants.ImagesPath + productImage.ImagePath);
            _productImageDal.Delete(productImage);
            return new SuccessResult();
        }
        public IResult Update(IFormFile file, ProductImage productImage)
        {
            productImage.ImagePath = _fileHelper.Update(file, PathConstants.ImagesPath + productImage.ImagePath, PathConstants.ImagesPath);
            _productImageDal.Update(productImage);
            return new SuccessResult();
        }

        public IDataResult<List<ProductImage>> GetAll()
        {
            return new SuccessDataResult<List<ProductImage>>(_productImageDal.GetAll());
        }

        public IDataResult<List<ProductImage>> GetByProductId(int productId)
        {
            var result = BusinessRules.Run(CheckCarImage(productId));//gorsel var mi kontrolu
            if (result != null)//dikkat - brusiness run-başarılı ise bişey göndermez null verri-null başarılı demek yani
            {
                return new ErrorDataResult<List<ProductImage>>(GetDefaultImage(productId).Data);
            }

            return new SuccessDataResult<List<ProductImage>>(_productImageDal.GetAll(p => p.ProductId == productId));
        }

        public IDataResult<ProductImage> GetByProductImageId(int productImageId)
        {
            return new SuccessDataResult<ProductImage>(_productImageDal.Get(p => p.ProductImageId == productImageId));


        }
        //Aşağıdakiler kural methodlarımız
        private IResult CheckIfProductImageLimit(int productId)//limit kontrol kurali
        {
            var result = _productImageDal.GetAll(c => c.ProductId == productId).Count;
            if (result >= 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IResult CheckCarImage(int productId)
        {
            var result = _productImageDal.GetAll(c => c.ProductId == productId).Count;
            if (result > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        private IDataResult<List<ProductImage>> GetDefaultImage(int productId)
        {

            List<ProductImage> productImage = new List<ProductImage>();
            productImage.Add(new ProductImage { ProductId = productId, Date = DateTime.Now, ImagePath = "DefaultImage.jpg" });
            return new SuccessDataResult<List<ProductImage>>(productImage);
        }
        private IResult CheckImageSizeIsAcceptable (ProductImage productImage)
        {
            return null;
        }


    }
}
