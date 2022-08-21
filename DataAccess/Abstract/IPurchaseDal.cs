using Core.DataAccess.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPurchaseDal : IBaseRepository<Purchase>
    {
        public List<PurchaseDetailDto> GetDetails(Expression<Func<PurchaseDetailDto, bool>> filter = null);
        
    }
}
