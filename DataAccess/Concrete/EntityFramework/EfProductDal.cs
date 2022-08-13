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
    public class EfProductDal : EfEntityBaseRepository<Product, EfCoreDbContext>, IProductDal
    {
        
        public List< ProductDetailDto> GetAllProductsDetails()
        {
            using (EfCoreDbContext context = new EfCoreDbContext())
            {
                var result = from pr in context.Products
                             join cat in context.Categories on pr.CategoryId equals cat.CategoryId
                             join col in context.Colors on pr.ColorId equals col.ColorId
                             join sel in context.Sellers on pr.SellerId equals sel.SellerId

                             select new ProductDetailDto
                             {
                                 ProductId = pr.ProductId,
                                 CategoryId= pr.CategoryId,
                                 ProductName = pr.ProductName,
                                 CategoryName = cat.CategoryName,
                                 UnitsInStock = pr.UnitsInStock,
                                 Status=pr.Status,
                                 IsOfferable=pr.IsOfferable,
                                 IsSold=    pr.IsSold,
                                 Description=pr.Description,
                                 ColorId=pr.ColorId,
                                 ColorName=col.ColorName,
                                 SellerId=sel.SellerId,
                                 SellerName=sel.SellerName,



                                 ImagePath=(from m in context.ProductImages where m.ProductId== pr.ProductId select m.ImagePath).ToList(),

                             };
                return result.ToList();
            }

        }

        public List<ProductDetailDto> GetProductsBySellerId(int sellerId)
        {
            using (EfCoreDbContext context = new EfCoreDbContext())
            {
                var result = from pr in context.Products
                             join cat in context.Categories on pr.CategoryId equals cat.CategoryId
                             join col in context.Colors on pr.ColorId equals col.ColorId
                             join sel in context.Sellers on pr.SellerId equals sellerId

                             select new ProductDetailDto
                             {
                                 ProductId = pr.ProductId,
                                 CategoryId = pr.CategoryId,
                                 ProductName = pr.ProductName,
                                 CategoryName = cat.CategoryName,
                                 UnitsInStock = pr.UnitsInStock,
                                 Status = pr.Status,
                                 IsOfferable = pr.IsOfferable,
                                 IsSold = pr.IsSold,
                                 Description = pr.Description,
                                 ColorId = pr.ColorId,
                                 ColorName = col.ColorName,
                                 SellerId = sel.SellerId,
                                 SellerName = sel.SellerName,




                                 ImagePath = (from m in context.ProductImages where m.ProductId == pr.ProductId select m.ImagePath).ToList(),

                             };
                return result.ToList();
            }

        }
    }
}
