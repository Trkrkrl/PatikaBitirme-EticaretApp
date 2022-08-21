using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog.Logger;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [CacheRemoveAspect("IUserService.Get")]
        [LogAspect(typeof(FileLogger))]
        public Result Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IUserService.Get")]
        [LogAspect(typeof(FileLogger))]
        public Result Delete(User user)
        {
           _userDal.Delete(user);   
            return new Result(true, Messages.UserDeleted);
        }
        [CacheRemoveAspect("IUserService.Get")]
        [LogAspect(typeof(FileLogger))]
        public Result Update(User user)
        {
            _userDal.Update(user);
            return new Result(true);
        }

        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public DataResult<User> GetByMail(string email)
        {
            var result = _userDal.GetAll(u => u.Email == email).FirstOrDefault();
            return new SuccessDataResult<User>(result);
        }
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public DataResult<User> GetByUserName(string userName)
        {
            var result = _userDal.GetAll(u => u.UserName == userName).FirstOrDefault();
            return new SuccessDataResult<User>(result);
        }
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public DataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.GetAll(u => u.UserId == userId).FirstOrDefault());
        }

        [LogAspect(typeof(FileLogger))]
        public Result UpdatePassword(UserPasswordUpdateDto userPasswordUpdateDto, int userId)
        {
            var user = (GetById(userId)).Data;
            if (!HashingHelper.VerifyPasswordHash(userPasswordUpdateDto.OldPassword, user.passwordHash, user.passwordSalt))
            {
                return new ErrorResult(Messages.OldPasswordIsWrong);

            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userPasswordUpdateDto.NewPassword, out passwordHash, out passwordSalt);


            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            user.Status = "active";
            _userDal.Update(user);


            return new SuccessDataResult<User>(user, Messages.PasswordUpdated);
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }


    }
}
