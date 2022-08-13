using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOfferDal: EfEntityBaseRepository<Offer, EfCoreDbContext>, IOfferDal
    {

        public List<OfferDetailDto> GetOfferDetails(int offerId)
        {
            
                using (EfCoreDbContext context = new EfCoreDbContext())
                {
                    var result = from of in context.Offers
                                 join pr in context.Products on of.ProductId equals pr.ProductId
                                 join selus in context.Users on of.ReceiverUserId equals selus.UserId
                                 join buyus in context.Users on of.SenderUserId equals buyus.UserId

                                 select new OfferDetailDto
                                 {
                                     OfferId=of.OfferId,


                                     OfferPercentage=of.OfferPercentage,
                                     OfferAmount=of.OfferAmount,

                                     ProductId=of.ProductId,
                                     offerStatus = of.offerStatus,

                                     SenderUserId=of.SenderUserId,
                                     SenderFirstName= buyus.FirstName,
                                     SenderLastName= buyus.LastName,

                                     ReceiverUserId=of.ReceiverUserId,
                                     ReceiverName=selus.FirstName,
                                     ReceiverLastName= selus.LastName,
                                    



                                     

                                 };
                    return result.ToList();
                
                }
        }
    }
}
