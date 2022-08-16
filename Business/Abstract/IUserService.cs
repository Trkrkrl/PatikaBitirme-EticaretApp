using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        DataResult<User> GetByMail(string email);
        DataResult<User> GetByUserName(string userName);
        DataResult<User> GetById(int userId);


        Result Add(User user);

        //delete olmali mi?
        Result Update(User user);

        Result Delete(User user);
        Result UpdatePassword(UserPasswordUpdateDto userPasswordUpdateDto, int userId);
        IDataResult<List<OperationClaim>> GetClaims(User user);


    }
}
