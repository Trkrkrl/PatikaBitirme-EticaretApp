using Business.Abstract;
using Business.Constants;
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

        public PurchaseManager(IPurchaseDal purchaseDal)
        {
            _purchaseDal = purchaseDal;
        }

        public IResult Add(Purchase purchase)
        {
            _purchaseDal.Add(purchase);
            return new SuccessResult(Messages.PurchaseAdded);
        }

        public IResult CancelPurchase(Purchase purchase)//delete
        {
            _purchaseDal.Delete(purchase);
            return new SuccessResult(Messages.PurchaseCanceled);
        }

        public DataResult<List<Purchase>> GetAll()
        {
            return new SuccessDataResult<List<Purchase>>(_purchaseDal.GetAll(),Messages.PurchasesListed);
        }

        public DataResult<List<Purchase>> GetByCustomerUserId(int userId)
        {
            return new SuccessDataResult<List<Purchase>>(_purchaseDal.GetAll(p=>p.UserId==userId), Messages.PurchasesListed2);

        }

        public DataResult< List<PurchaseDetailDto>> GetByDetailsByPurchaseId(int purchaseId)
        {
            return new SuccessDataResult<List<PurchaseDetailDto>>(_purchaseDal.GetByDetailsByPurchaseId(purchaseId), Messages.PurchasesListed3);
        }
    }
}
