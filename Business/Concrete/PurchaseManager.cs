using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog.Logger;
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
    public class PurchaseManager : IPurchaseService
    {/*ilgili ürün satın alınınca isSold durumunu =>True yap
      * 
      * 
      * 
      *
      */
        private readonly IPurchaseDal _purchaseDal;
        private readonly IProductService _productService;
        private readonly IAddressService _addressService;
        

        public PurchaseManager(IPurchaseDal purchaseDal, IProductService productService,IAddressService adressService)
        {
            _purchaseDal = purchaseDal;
            _productService = productService;
            _addressService = adressService;
        }

        [CacheRemoveAspect("IPurchaseService.Get")]
        [LogAspect(typeof(FileLogger))]
        public IResult Add(Purchase purchase)
        {//
            var specimenProductId=purchase.ProductId;
            var specimen = _productService.GetById(specimenProductId);
            specimen.Data.IsSold = true;

            _purchaseDal.Add(purchase);
            return new SuccessResult(Messages.PurchaseAdded);
        }
        [CacheRemoveAspect("IPurchaseService.Get")]
        [LogAspect(typeof(FileLogger))]
        public IResult CancelPurchase(Purchase purchase)//delete
        {
            var specimenProductId = purchase.ProductId;
            var specimen = _productService.GetById(specimenProductId);
            specimen.Data.IsSold = false;
            _purchaseDal.Delete(purchase);
            return new SuccessResult(Messages.PurchaseCanceled);
        }

        [CacheAspect]
        [LogAspect(typeof(FileLogger))]

        public DataResult<List<Purchase>> GetAll()
        {
            return new SuccessDataResult<List<Purchase>>(_purchaseDal.GetAll(),Messages.PurchasesListed);
        }
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]

        public DataResult<List<Purchase>> GetByCustomerUserId(int userId)
        {
            return new SuccessDataResult<List<Purchase>>(_purchaseDal.GetAll(p=>p.UserId==userId), Messages.PurchasesListed2);

        }
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public DataResult< List<PurchaseDetailDto>> GetByDetailsByPurchaseId(int purchaseId)
        {
            return new SuccessDataResult<List<PurchaseDetailDto>>(_purchaseDal.GetByDetailsByPurchaseId(purchaseId), Messages.PurchasesListed3);
        }
        [CacheRemoveAspect("IPurchaseService.Get")]
        [LogAspect(typeof(FileLogger))]
        public IResult AddFromOffers(Offer offer)
        {//
            IDataResult<Address> addressResult = _addressService.GetByUserId(offer.SenderUserId);
            int usersFirstAddressId = addressResult.Data.AddressId;

            Purchase purchase = new Purchase()
            {
                ProductId = offer.ProductId,
                SellerId=offer.ReceiverUserId,
                OrderDate=DateTime.Now,
                TotalAmount=offer.OfferAmount,
                UserId=offer.SenderUserId,
                AdressId=usersFirstAddressId

            };


            _purchaseDal.Add(purchase);
            return new SuccessResult(Messages.PurchaseFromOfferAdded);
        }
    }
}
