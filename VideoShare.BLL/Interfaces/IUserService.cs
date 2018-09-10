using System.Collections.Generic;
using System.Threading.Tasks;
using VideoShare.BLL.Infrastructure;
using VideoShare.BLL.DTO;
using System.Security.Claims;
using System;
using Microsoft.AspNet.Identity;
using VideoShare.DAL.Entities;

namespace VideoShare.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> CreateAsync(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task<bool> IsEmailConfirmed(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
        Task SendEmailAsync(string userId, string subject, string message);
        IIdentityMessageService EmailService { get; set; }
        IUserTokenProvider<ApplicationUser, string> UserTokenProvider { get; set; }
        Task<UserDTO> FindAsync(string email, string password);
        Task<IdentityResult> ConfirmEmail(string userId, string token); 
        void SetDefaultTokenProvider();
    }
}