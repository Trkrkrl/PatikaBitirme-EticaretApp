using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto); //Kayıt operasyonu
        IDataResult<User> LoginWithEmail(UserMailLoginDto userMailLoginDto);//mail ile Giriş operasyonu
        IDataResult<User> LoginWithUserName(UserNameLoginDto userNameLoginDto);//username ile Giriş operasyonu


        
        DataResult<AccessToken> CreateAccessToken(User user);
    }
}
