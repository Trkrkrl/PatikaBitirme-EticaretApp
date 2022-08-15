using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
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

        private readonly IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IUserDal _userDal;

        public AuthManager(IUserDal userDal,IUserService userService,ITokenHelper tokenHelper)
        {
            _userService = userService;
            _userDal = userDal;
            _tokenHelper = tokenHelper;
        }

        [ValidationAspect(typeof(UserResigterValidator))]

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
           IResult result = BusinessRules.Run(
               IsUserNameUnique(userForRegisterDto.UserName),
               IsEmailUnique(userForRegisterDto.Email)


                );
            if (result != null)
            {
                return new ErrorDataResult<User>(Messages.CouldNotCreateUser);
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User//bu account core entitiesteki
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                UserName = userForRegisterDto.UserName,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt,
                Status = "active"
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }



        [ValidationAspect(typeof(UserLoginWithMailValidator))]

        public IDataResult<User> LoginWithEmail(UserMailLoginDto userMailLoginDto)
        {
            var userToCheck = _userService.GetByMail(userMailLoginDto.Email);

            if(userToCheck.Data.EndOfSuspension <= DateTime.Now && userToCheck.Data.Status == "suspended")//engel tarihi geçmişse statüyü akctive et ve giriş yap
            {
                userToCheck.Data.Status = "active";
            }
            if (userToCheck.Data.Status == "active")//giriş yap
            {
                if (userToCheck.Data == null)
                {
                    return new ErrorDataResult<User>(Messages.UserNotFound);
                }

                if (!HashingHelper.VerifyPasswordHash(userMailLoginDto.Password, userToCheck.Data.passwordHash, userToCheck.Data.passwordSalt))
                {
                    userToCheck.Data.FailedRecentLoginAttempts += 1;
                    if (userToCheck.Data.FailedRecentLoginAttempts == 3)
                    {
                        userToCheck.Data.Status = "suspended";

                        userToCheck.Data.FailedRecentLoginAttempts = 0;
                        userToCheck.Data.EndOfSuspension = DateTime.Now.AddMinutes(10);
                        //send info mail to message service
                    }

                    return new ErrorDataResult<User>(Messages.PasswordError);
                }

                return new SuccessDataResult<User>(Messages.SuccessfulLogin);

            }
            return new ErrorDataResult<User> (Messages.UserSuspended);
        }
        
        [ValidationAspect(typeof(UserLoginWithUserNameValidator))]

        public IDataResult<User> LoginWithUserName(UserNameLoginDto userNameLoginDto)
        {
            var userToCheck = _userService.GetByUserName(userNameLoginDto.UserName);
           
            if (userToCheck.Data.EndOfSuspension <= DateTime.Now&& userToCheck.Data.Status == "suspended")//engel tarihi geçmişse statüyü akctive et ve giriş yap
            {              
                userToCheck.Data.Status = "active";
            }
            if (userToCheck.Data.Status == "active")//giriş yap
            {
                if (userToCheck.Data == null)
                {
                    return new ErrorDataResult<User>(Messages.UserNotFound);
                }

                if (!HashingHelper.VerifyPasswordHash(userNameLoginDto.Password, userToCheck.Data.passwordHash, userToCheck.Data.passwordSalt))
                {
                    userToCheck.Data.FailedRecentLoginAttempts += 1;
                    if (userToCheck.Data.FailedRecentLoginAttempts == 3)
                    {
                        userToCheck.Data.Status = "suspended";
                       
                        userToCheck.Data.FailedRecentLoginAttempts = 0;
                        userToCheck.Data.EndOfSuspension = DateTime.Now.AddMinutes(10);
                        //send info mail to message service
                    }

                    return new ErrorDataResult<User>(Messages.PasswordError);
                }

                return new SuccessDataResult<User>(Messages.SuccessfulLogin);

            }
            return new ErrorDataResult<User>(Messages.UserSuspended);

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
        //--
        private IResult IsEmailUnique(string email)//böyle bir email adresine kayıtlı kullanıcımız var mı?
        {
            var result = _userService.GetByMail(email).Success;
            if (!result)
            {
                return new ErrorResult(Messages.EmailOnUse);

            }
            return new Result(result);
        }
        private IResult IsUserNameUnique(string userName)//böyle bir kullanıcı adı ile kayıtlı kullanıcımız var mı?
        {
            var result = _userService.GetByUserName(userName).Success;
            if (!result)
            {
                return new ErrorResult(Messages.UserNameExists);

            }
            return new Result(result);


        }
    }
}
