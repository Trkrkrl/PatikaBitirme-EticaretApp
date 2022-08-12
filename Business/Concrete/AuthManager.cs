using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {

        //register başarılı ise mail gönderme servisine kuyruğuna mail ilet
        // login 3 defa başarısız ise mail göndersin
        // register ve login için gerekli karakter sayısı validasyonları ve mesajları

        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IUserDal _userDal;

        public AuthManager(IUserDal userDal,IUserService userService,ITokenHelper tokenHelper)
        {
            _userService = userService;
            _userDal = userDal;
            _tokenHelper = tokenHelper;
        }


        public DataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var account = new User//bu account core entitiesteki
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                UserName = userForRegisterDto.UserName,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(account, Messages.UserRegistered);
        }

        public DataResult<User> LoginWithEmail(UserMailLoginDto userMailLoginDto)
        {
            var userToCheck = _userService.GetByMail(userMailLoginDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userMailLoginDto.Password, userToCheck.Data.passwordHash, userToCheck.Data.passwordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        public DataResult<User> LoginWithUserName(UserNameLoginDto userNameLoginDto)
        {
            var userToCheck = _userService.GetByUserName(userNameLoginDto.UserName);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userNameLoginDto.Password, userToCheck.Data.passwordHash, userToCheck.Data.passwordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        

        public Result UserExists(string email)
        {
            var result = _userDal.Any(x => x.Email == email);
            if (!result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        public DataResult<AccessToken> CreateAccessToken(User user)
        {
            var accessToken = _tokenHelper.CreateToken(user);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
