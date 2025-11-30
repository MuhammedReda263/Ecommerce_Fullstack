using AutoMapper;
using Ecom.API.Helpers;
using Ecom.Core.DTO;
using Ecom.Core.Entities;
using Ecom.Core.Entities.Order;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecom.API.Controllers
{

    public class AccountController : BaseController
    {

        public AccountController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }


        [Authorize]
        [HttpGet("role")]
        public IActionResult GetRole()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            return Ok(new { role });
        }


        [HttpGet("isAuth")]
        public IActionResult isAuth() => User.Identity!.IsAuthenticated ? Ok() : Unauthorized();

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

        [HttpPost("forget-password")]
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

        [HttpPut("update-address")]
        public async Task<IActionResult> updateAddress (ShippingAddress shippingAddress)
        {
            var updatedAddress = _mapper.Map<Address>(shippingAddress);
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _unitOfWork.auth.UpdateAddress(userEmail!,updatedAddress);
            return result ? NoContent() : BadRequest();

        }

        [HttpGet("get-address-for-user")]
        public async Task<IActionResult> getAddress ()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var address = await _unitOfWork.auth.getUserAddress(userEmail!);
            var result = _mapper.Map<ShipAddressDTO>(address);
            return Ok(result);

        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token", new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None
            });

            return Ok(new { message = "Logged out successfully" });
        }





    }
}
