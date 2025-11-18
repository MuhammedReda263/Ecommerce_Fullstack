using Ecom.Core.DTO;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Core.Sharing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Ecom.Infrastructure.Repositories
{
    public class AuthRepository : IAuth
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IGenerateToken _generateToken;
        
        public AuthRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService,IGenerateToken generateToken)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _generateToken = generateToken;

        }
        public async Task<string?> RegisterAsync(RegisterDTO registerDTO)
        {
            if (registerDTO == null) return null;
            if (await _userManager.FindByNameAsync(registerDTO.UserName) is not null)
                return "Username already exists";
            if (await _userManager.FindByEmailAsync(registerDTO.Email) is not null)
                return "Email already exists";
            var user = new AppUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return errors;
            }
            string code =  await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await SendEmail(registerDTO.Email, code, "active", "Active Mail", "Please active your email, click on button to active");
            return "Success";


        }

        public async Task SendEmail(string email, string code, string component, string subject, string message)
        {
            
            EmailDTO emailDTO = new EmailDTO 
            (
                email,
                "moreda263@gmail.com",
                subject,
                EmailStringBody.Send(email, code, component, message)
            );
           await _emailService.SendEmailAsync(emailDTO);

        }

        public async Task<string?> LoginAsync(loginDTO login)
        {
            if (login == null)
            {
                return null;
            }
            var finduser = await _userManager.FindByEmailAsync(login.Email);

            if(finduser != null)
            {
                if (!finduser.EmailConfirmed)
                {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(finduser);

                    await SendEmail(finduser.Email!, token, "active", "ActiveEmail", "Please active your email, click on button to active");

                    return "Please confirem your email first, we have sent activatin link to your E-mail";
                }
                var result = await _signInManager.CheckPasswordSignInAsync(finduser, login.Password, true);

                if (result.Succeeded) return _generateToken.GetAndCreateTokenAsync(finduser);
                else return "Please check your email and password, something went wrong";
            }
            

            return "Please check your email and password, something went wrong";
        }

        public async Task<bool> SendEmailForForgetPassword(string email)
        {
            var findUser = await _userManager.FindByEmailAsync(email);
            if (findUser == null) return false;
            var token = await _userManager.GeneratePasswordResetTokenAsync(findUser);
            await SendEmail(findUser.Email!, token, "Reset-Password", "Rest pssword", "click on button to Reset your password");
            return true;

        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDTO dto)
        {
            var findUser = await _userManager.FindByEmailAsync(dto.Email);
            if (findUser == null) return false;
            var result = await _userManager.ResetPasswordAsync(findUser, dto.Token, dto.NewPassword);
            return result.Succeeded;
        }

        public async Task<bool> ActiveAccount(ActiveAccountDTO accountDTO)
        {
            var findUser = await _userManager.FindByEmailAsync(accountDTO.Email);
            if (findUser == null) return false;
            var result = await _userManager.ConfirmEmailAsync(findUser, accountDTO.Token);
            if (!result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(findUser);
                await SendEmail(accountDTO.Email, token, "active", "Active Mail", "Please active your email, click on button to active");
                return false;
            }
            return true;

        }

      
    }
}
