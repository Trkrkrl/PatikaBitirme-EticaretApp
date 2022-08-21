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
    public interface IPurchaseService
    {
        IResult CancelPurchase(Purchase purchase);
        IResult Add(Purchase purchase);
        DataResult<Purchase> GetByDetailsByPurchaseId(int purchaseId);
        DataResult<List<Purchase>> GetByCustomerUserId(int userId);
        DataResult<List<Purchase>> GetAll();
        IResult AddFromOffers(Offer offer);
        
    }
}
