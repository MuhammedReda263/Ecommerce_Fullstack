using Ecom.Core.DTO;
using Ecom.Core.Entities;
using Ecom.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IAuth
    {
        Task<string?> RegisterAsync(RegisterDTO registerDTO);
        Task SendEmail(string email, string code, string component, string message, string subject);
        Task<string?> LoginAsync(loginDTO login);
        Task<bool> SendEmailForForgetPassword(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordDTO dto);
        Task<bool> ActiveAccount(ActiveAccountDTO accountDTO);
    }
}
