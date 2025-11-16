using Ecom.Core.DTO;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Core.Sharing;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class AuthRepository : IAuth
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        
        public AuthRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;

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
                Email = registerDTO.Email
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

        public async Task SendEmail(string email, string code, string component, string message, string subject)
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
    } 
}
