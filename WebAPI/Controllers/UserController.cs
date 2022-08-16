using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("getbymail")]
        [Authorize]
        public IActionResult GetByMail(string email)
        {
            var result = _userService.GetByMail(email);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyusername")]
        [Authorize]
        public IActionResult GetByUserName(string userName)
        {
            var result = _userService.GetByUserName(userName);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }



        //post-add,update ,delete
        [HttpPost("add")]
        [Authorize]
        public IActionResult Add(User user)
        {

            var result = _userService.Add(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("updatepassword")]
        [Authorize]
        public IActionResult Update(UserPasswordUpdateDto userPasswordUpdateDTO)
        {
            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int userId = int.Parse(clm);

            var result = _userService.UpdatePassword(userPasswordUpdateDTO, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        [Authorize]
        public IActionResult Delete(User user)
        {
            var clm = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int userId = int.Parse(clm);
            if (userId==user.UserId)
            {
                var result = _userService.Delete(user);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(Messages.UnAuthorizedDeleteAttempt);
            

        }
    }
}
