using Core.DataAccess.Concrete.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPurchaseDal : EfEntityBaseRepository<Purchase, EfCoreDbContext>, IPurchaseDal
    {
        public List<PurchaseDetailDto> GetDetails(Expression<Func<PurchaseDetailDto, bool>> filter = null)
        {
            using (EfCoreDbContext context = new EfCoreDbContext())
            {
                var result = from pur in context.Purchases
                             join pr in context.Products on pur.ProductId equals pr.ProductId
                            
                             join buyus in context.Users on pur.UserId equals buyus.UserId



                             select new PurchaseDetailDto
                             {
                                 PurchaseId = pur.PurchaseId,
                                 ProductId = pur.ProductId,
                                 ProductName = pr.ProductName,
                                 UserId = pur.UserId,
                                 SellerId = pr.SellerId,
                                 OrderDate = DateTime.Now,//bunu burada yapmam ne kadar doğru

                                 TotalAmount = pur.TotalAmount,
                                 BuyerFirstName = buyus.FirstName,
                                 BuyerLastName = buyus.LastName,
                                 Email = buyus.Email,
                                 PhoneNumber = buyus.PhoneNumber,

                                 DeliveryAdress = ((Address)(from adr in context.Addresses where adr.UserId == buyus.UserId select adr).FirstOrDefault())//??burdan şüpheliyim-ki patladı hemen




                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();

            }
        }
    }
}
