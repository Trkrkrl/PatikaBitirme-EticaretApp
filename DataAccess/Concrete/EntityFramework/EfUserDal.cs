using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal: EfEntityBaseRepository<User, EfCoreDbContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new EfCoreDbContext())
            {
                var result = from oclaims in context.OperationClaims
                             join userOperationClaims in context.UserOperationClaims on oclaims.Id equals userOperationClaims.OperationClaimId
                             

                             where userOperationClaims.UserId == user.UserId

                             select new OperationClaim
                             { 
                                 Id = oclaims.Id, 
                                 Name = oclaims.Name 
                             };
                return result.ToList();

            }
        }
    }
}
