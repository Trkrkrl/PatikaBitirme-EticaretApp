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
    public class OfferManager:IOfferService
    {/*is offerable kontrolü-okw
      * teklif yüzde=>fiyat , fiyat=>yüzde işlemleri -ok
      * kullanıcı teklif iptal edebilsin-ok
      * kullanıcı profilinde görünmesi için yaptığı ve ona yapılan teklifler-ok
    
        ------
      * eğer 100 ise offerstatus :accepted olsun=direkt purchase managere gitsin --bunu purchase ten sonra bir düşünecem
     
      * Bir de satıcı tarafında offer kabul etme durumu olacak
      * kabul ederse söz konusu offer tamamlanır- => durumu accpeted olur=> purchase managere gitsin - bu ve yukardaki 100 durumlarını bağlayabilrim
      * 
      * 
      */
        private readonly IOfferDal _offerDal;
        private readonly IProductService _productService;
        private readonly IPurchaseService _purchaseService;
        public OfferManager(IOfferDal offerDal,IProductService  productService,IPurchaseService purchaseService)
        {
            _offerDal = offerDal;
            _productService = productService;
            _purchaseService = purchaseService;
        }
        //add(make), delete, update, getall, getsentoffersbyuserid,getreceivedoffersbyuserid,
        //offer detaildtoda gönderen ve alıcı detayını ekledim

        public IResult Add (Offer offer)//teklif yap 
        {   
            var isOfferable = _productService.CheckOfferable(offer.ProductId).Success;//resultun succes parçasını alıyoruz-illa başarılı olacak deil
            var productId=offer.ProductId;  
            var senderUserId=offer.SenderUserId;
            var productPrice = _productService.GetProductPriceById(productId);
            //-
            if (offer.OfferPercentage==0)//fiyat girerse yüzdeyi biz hesaplarız
            {
                offer.OfferPercentage = (double)(offer.OfferAmount / productPrice);
            } else if (offer.OfferAmount == 0)//yüzde seçtiyse teklif fiyatını biz hesaplarız
            {
                offer.OfferAmount = (int)((offer.OfferPercentage / 100 )  * productPrice);
            }
            //-
            if (!isOfferable)
            {
                return new ErrorResult(Messages.ProductIsNotOfferable);
            }
            //-
            
            

                IResult result = BusinessRules.Run(

                    CheckIfUserMadeOfferBefore(senderUserId, productId)
                
                    );

                if (result != null)
                {
                    return result;
                }
                if(offer.OfferPercentage == 100)
                {
                offer.offerStatus = "accepted";
                _purchaseService.AddFromOffers(offer);
                return new SuccessResult(Messages.OfferAcceptedAndReliedToPurchase);
                }
                else
                {
                    _offerDal.Add(offer);
                    return new SuccessResult(Messages.OfferSent);
                }

            
        }
        public IResult Delete(Offer offer)//teklifi geri almak amacıyla gönderen kişi tarafından silinebilmeli
        {
            var offerId = offer.OfferId;
            
            IResult result = BusinessRules.Run(

               CheckIfOfferIsCancellable(offerId)

               );

            if (result != null)
            {
                return result;

            }
            _offerDal.Delete(offer);
            return new SuccessResult(Messages.OfferDeleted);
        }

        public IResult Update(Offer offer)
        {
            var offerId=offer.OfferId;
            IResult result = BusinessRules.Run(

               CheckIfOfferIsUpdatable(offerId)

               );

            if (result != null)
            {
                return result;

            }
            
            _offerDal.Update(offer);
            return new SuccessResult(Messages.OfferUpdated);
        }
        //--

        public IDataResult<Offer> GetByOfferId(int offerId)
        {

            return new SuccessDataResult<Offer>(_offerDal.Get(o=>o.OfferId==offerId));
        }
        public IDataResult<List<Offer>> GetReceivedBySellerId(int sellerUserId)//satıcının aldığı teklifler
        {
            return new SuccessDataResult<List<Offer>>(_offerDal.GetAll(o=>o.ReceiverUserId== sellerUserId));
        }
        public IDataResult<List<Offer>> GetSentByCustomerId(int customerUserId)//müşterinin yaptıkları
        {
            return new SuccessDataResult<List<Offer>>(_offerDal.GetAll(o=>o.SenderUserId== customerUserId));

        }
        public IDataResult<List<Offer>> GetAll()//Eğer admin vs varsa tüm teklifleri görmesi için-buna admin attribute gelmeli
        {
            return new SuccessDataResult<List<Offer>>(_offerDal.GetAll());//girileccek
     
        }
        public IDataResult<List<OfferDetailDto>> GetOfferDetailsByOfferId(int offerId)
        {
           return new SuccessDataResult<List<OfferDetailDto>> (_offerDal.GetOfferDetails(offerId));
        }
        public IResult AcceptOffer (Offer offer)
        {
            _purchaseService.AddFromOffers(offer);
            return new SuccessResult(Messages.OfferAccepted);
        }



        //
        //kurallar

        //kural 1 teklif yaptığı teklifleri listesini getir ve içerisinede bu ürüne yapılmış teklif varmı ve bu teklif - on pending mi
        private IResult CheckIfUserMadeOfferBefore(int senderUserId,int productId)
        {
            var result = _offerDal.GetAll(o => o.SenderUserId==senderUserId && o.ProductId==productId && o.offerStatus=="pending").Any();//bu kişinin-bu ürüne- beklemede olan teklifi varmı
            if (!result)
            {
                return new SuccessResult();

            }//varsa devamet
            return new ErrorResult(Messages.OfferIsPending);

        }
        //kural 2: teklif geri alınabilir durumdamı
        private IResult CheckIfOfferIsCancellable(int offerId)
        {
            //yaptığı teklif kabul edilmiş mi
            var result = _offerDal.GetAll(o => o.OfferId == offerId &&  o.offerStatus == "accepted").Any();
            if (!result)
            {
                return new SuccessResult();

            }
            return new ErrorResult(Messages.OfferIsAccepted2);

        }
        //kural 3 - güncellenebilirmi?
        private IResult CheckIfOfferIsUpdatable(int offerId)
        {
            //yaptığı teklif kabul edilmiş mi
            var result = _offerDal.GetAll(o => o.OfferId == offerId && o.offerStatus == "accepted").Any();
            if (!result)
            {
                return new SuccessResult();

            }
            return new ErrorResult(Messages.OfferIsAccepted3);

        }

        
    }

    
}
