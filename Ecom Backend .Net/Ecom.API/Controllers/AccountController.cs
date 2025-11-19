using AutoMapper;
using Ecom.API.Helpers;
using Ecom.Core.DTO;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register( RegisterDTO registerDTO)
        {
           var result = await _unitOfWork.auth.RegisterAsync(registerDTO);
            if (result == "Success")
                return Ok(new ResponseAPI (200,result));
            return BadRequest(new ResponseAPI(400, result));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login( loginDTO loginDTO)
        {
           var result = await _unitOfWork.auth.LoginAsync(loginDTO);
            if (result!.StartsWith("Please"))
            {
                return BadRequest(new ResponseAPI(400, result));
            }
             Response.Cookies.Append("token", result, new CookieOptions
             {
                 HttpOnly = true,
                 Secure = true,
                 SameSite = SameSiteMode.None,
                 IsEssential = true,
                 Domain = "localhost",
                 Expires = DateTime.Now.AddDays(1)
             });
            return Ok(new ResponseAPI(200,"Login Success"));
        }

        [HttpPost("active")]
        public async Task<ActionResult<ActiveAccountDTO>> active(ActiveAccountDTO accountDTO)
        {
            var result = await _unitOfWork.auth.ActiveAccount(accountDTO);
            return result ? Ok(new ResponseAPI(200, "Activation Successed")) : BadRequest(new ResponseAPI(400, "Activation Failed"));
        }

        [HttpGet("forget-password")]
        public async Task<IActionResult> forget(ForgetPasswordDTO forgetPasswordDTO)
        {
            var result = await _unitOfWork.auth.SendEmailForForgetPassword(forgetPasswordDTO.Email);
            return result ? Ok(new ResponseAPI(200,"Email sent Successfully")) : BadRequest(new ResponseAPI(400,"User not found"));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> reset(ResetPasswordDTO restPasswordDTO)
        {
            var result = await _unitOfWork.auth.ResetPasswordAsync(restPasswordDTO);
            if (!result) return BadRequest(new ResponseAPI(400,"User Not Found"));         
            return Ok(new ResponseAPI(200,"Password Reseted Successfully"));
        }
    }
}
