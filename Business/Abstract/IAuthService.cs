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
        IResult Register(UserForRegisterDto userForRegisterDto); //Kayıt operasyonu
        IResult LoginWithEmail(UserMailLoginDto userMailLoginDto);//mail ile Giriş operasyonu
        IResult LoginWithUserName(UserNameLoginDto userNameLoginDto);//username ile Giriş operasyonu


        Result UserExists(string email);//Kullanıcı var mı
        DataResult<AccessToken> CreateAccessToken(User user);
    }
}
