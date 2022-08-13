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
    public interface IOfferService
    {
        IResult Add(Offer offer);
        IResult Delete(Offer offer);
        IResult Update(Offer offer);
        IDataResult<Offer> GetByOfferId(int offerId);
        IDataResult<List<Offer>> GetReceivedBySellerId(int sellerUserId);
        IDataResult<List<Offer>> GetSentByCustomerId(int customerUserId);
        IDataResult<List<Offer>> GetAll();
        IDataResult<List<OfferDetailDto>> GetOfferDetailsByOfferId(int offerId);





    }
}
