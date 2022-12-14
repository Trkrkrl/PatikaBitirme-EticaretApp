using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.SeriLog.Logger;
using Core.Utilities.Business;
using Core.Utilities.Mail;
using Core.Utilities.MessageBrokers.RabbitMQ;
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
      * eğer 100 ise offerstatus :accepted olsun-ok
     
      * Bir de satıcı tarafında offer kabul etme durumu -ok
      * kabul ederse söz konusu offer tamamlanır- => durumu accpeted olur=> purchase managere gitsin - ok
      * 
      * 
      */
        private readonly IOfferDal _offerDal;
        private readonly IProductService _productService;
        private readonly IPurchaseService _purchaseService;
        private readonly IUserService _userService;
        private readonly IMessageBrokerHelper _messageBrokerHelper;
        public OfferManager(IOfferDal offerDal,IProductService  productService,IPurchaseService purchaseService,IUserService userService,IMessageBrokerHelper messageBrokerHelper)
        {
            _offerDal = offerDal;
            _productService = productService;
            _purchaseService = purchaseService;
            _userService = userService; 
            _messageBrokerHelper = messageBrokerHelper;
        }
        //add(make), delete, update, getall, getsentoffersbyuserid,getreceivedoffersbyuserid,
        //offer detaildtoda gönderen ve alıcı detayını ekledim
        [ValidationAspect(typeof(OfferValidator))]
        [CacheRemoveAspect("IOfferService.Get")]
        [LogAspect(typeof(FileLogger))]
        public IResult Add (Offer offer)//teklif yap 
        {   
            var isOfferable = _productService.CheckOfferable(offer.ProductId).Success;//resultun succes parçasını alıyoruz-illa başarılı olacak deil
            var productId=offer.ProductId;  
            offer.ReceiverUserId=_productService.GetUserIdByProductId(productId);
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
                if(offer.OfferPercentage == 100||offer.OfferAmount>=productPrice)
                {
                offer.offerStatus = "accepted";
                Update(offer);
                _purchaseService.AddFromOffers(offer);
                return new SuccessResult(Messages.OfferAcceptedAndReliedToPurchase);
                }
                else
                {
                    offer.offerStatus="pending";
                    _offerDal.Add(offer);
                    return new SuccessResult(Messages.OfferSent);
                }

            
        }
            [CacheRemoveAspect("IOfferService.Get")]
            [LogAspect(typeof(FileLogger))]
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
        [CacheRemoveAspect("IOfferService.Get")]
        [LogAspect(typeof(FileLogger))]
        [ValidationAspect(typeof(OfferValidator))]
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
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<Offer> GetByOfferId(int offerId)
        {

            return new SuccessDataResult<Offer>(_offerDal.Get(o=>o.OfferId==offerId));
        }
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Offer>> GetReceivedBySellerId(int sellerUserId)//satıcının aldığı teklifler
        {
            return new SuccessDataResult<List<Offer>>(_offerDal.GetAll(o=>o.ReceiverUserId== sellerUserId));
        }
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Offer>> GetSentByCustomerId(int customerUserId)//müşterinin yaptıkları
        {
            return new SuccessDataResult<List<Offer>>(_offerDal.GetAll(o=>o.SenderUserId== customerUserId));

        }

        [SecuredOperation("admin")]
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Offer>> GetAll()
        {
            return new SuccessDataResult<List<Offer>>(_offerDal.GetAll());//girileccek
     
        }

        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
   public IDataResult<List<OfferDetailDto>> GetOfferDetailsByOfferId(int offerId)
        {
           return new SuccessDataResult<List<OfferDetailDto>> (_offerDal.GetOfferDetails(offerId));
        }

        [CacheRemoveAspect("IOfferService.Get")]
        [LogAspect(typeof(FileLogger))]
        public IResult AcceptOffer (Offer offer)
        {
            offer.offerStatus = "accepted";
            Update(offer);
            _purchaseService.AddFromOffers(offer);
            DeclineOtherOffers(offer.ProductId, offer.OfferId);//diğerlerini otomatik oalrak reddet
            //kabul edildi mail gönder
            var acceptedUser = _userService.GetById(offer.SenderUserId);

            EmailAddress accepteAddress = new EmailAddress { Address = acceptedUser.Data.Email, Name = acceptedUser.Data.FirstName };
            List<EmailAddress> emailAddresses = new List<EmailAddress>();
            emailAddresses.Add(accepteAddress);
            //-gönderilecek mail  hazırlanıyor

            EmailMessage acceptedNotificationMail = new EmailMessage()
            {

                Status = "sending",
                Content = Messages.OfferAcceptedMail,
                ToAddresses = emailAddresses,
                Subject = "Offer Accepted"

            };
            //buradan da email gönder kuyruğuna ekler
            _messageBrokerHelper.QueueEmail(acceptedNotificationMail);
            return new SuccessResult(Messages.OfferAccepted);
        }
        [CacheRemoveAspect("IOfferService.Get")]
        [LogAspect(typeof(FileLogger))]

        public IResult DeclineOffer(Offer offer)
        { offer.offerStatus = "declined";
            _offerDal.Update(offer);

            List<EmailAddress> emailAddresses = new List<EmailAddress>();
            var declinedUser = _userService.GetById(offer.SenderUserId);
            EmailAddress declinedAddress = new EmailAddress { Address = declinedUser.Data.Email, Name = declinedUser.Data.FirstName };
            emailAddresses.Add(declinedAddress);

            EmailMessage acceptedNotificationMail = new EmailMessage()
            {

                Status = "sending",
                Content = Messages.YourOfferDeclined,
                ToAddresses = emailAddresses,
                Subject = "Offer Declined"

            };
            //buradan da email gönder kuyruğuna ekler
            _messageBrokerHelper.QueueEmail(acceptedNotificationMail);

            return new SuccessResult(Messages.OfferDeclinedBySeller);
        }
        [CacheRemoveAspect("IOfferService.Get")]
        [LogAspect(typeof(FileLogger))]

        public IResult DeclineOtherOffers(int productId,int offerId)//diğer müşterilerin aynı ürüne yaptığı teklif reddedilir
        {
           List<Offer> otherOffers= _offerDal.GetAll(o => o.ProductId == productId);

           var safeOffer= GetByOfferId(offerId).Data;

            otherOffers.Remove(safeOffer);
            

            foreach (var of in otherOffers)
            {
                DeclineOffer(of);

            }

           

           
            return new SuccessResult(Messages.OtherOffersDeclinedAutomatically);

        }


        //
        //kurallar

        //kural 1 teklif yaptığı teklifleri listesini getir ve içerisinede bu ürüne yapılmış teklif varmı ve bu teklif - on pending mi
        private IResult CheckIfUserMadeOfferBefore(int senderUserId,int productId)
        {
            var result = _offerDal.GetAll(o => o.SenderUserId==senderUserId && o.ProductId==productId && o.offerStatus=="pending").Any();//bu kişinin-bu ürüne- beklemede olan teklifi varmı
            if (result)
            {
               return new ErrorResult(Messages.OfferIsPending);

            }//varsa devamet
             return new SuccessResult();

        }
        //kural 2: teklif geri alınabilir durumdamı
        private IResult CheckIfOfferIsCancellable(int offerId)
        {
            //yaptığı teklif kabul edilmiş mi
            var result = _offerDal.GetAll(o => o.OfferId == offerId &&  o.offerStatus == "accepted").Any();
            if (result)
            {
               return new ErrorResult(Messages.OfferIsAccepted2);

            }
             return new SuccessResult();

        }
        //kural 3 - güncellenebilirmi?
        private IResult CheckIfOfferIsUpdatable(int offerId)
        {
            //yaptığı teklif kabul edilmiş mi
            var result = _offerDal.GetAll(o => o.OfferId == offerId && o.offerStatus == "accepted").Any();
            if (result)
            {
                return new ErrorResult(Messages.OfferIsAccepted3);

            }
            return new SuccessResult();

        }

        
    }

    
}
