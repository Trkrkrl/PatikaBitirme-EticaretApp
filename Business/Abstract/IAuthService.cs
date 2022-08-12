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
        DataResult<User> Register(UserForRegisterDto userForRegisterDto); //Kayıt operasyonu
        DataResult<User> LoginWithEmail(UserMailLoginDto userMailLoginDto);//mail ile Giriş operasyonu
        DataResult<User> LoginWithUserName(UserNameLoginDto userNameLoginDto);//username ile Giriş operasyonu


        Result UserExists(string email);//Kullanıcı var mı
        DataResult<AccessToken> CreateAccessToken(User user);
    }
}
