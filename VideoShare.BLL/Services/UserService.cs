using VideoShare.BLL.DTO;
using VideoShare.BLL.Infrastructure;
using VideoShare.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using VideoShare.BLL.Interfaces;
using VideoShare.DAL.Interfaces;
using VideoShare.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.Owin;

namespace VideoShare.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public IIdentityMessageService EmailService
        {
            get
            {
                return Database.UserManager.EmailService;
            }

            set
            {
                Database.UserManager.EmailService = value;
            }
        }

        public IUserTokenProvider<ApplicationUser, string> UserTokenProvider
        {
            get
            {
                return Database.UserManager.UserTokenProvider;
            }
            set
            {
                Database.UserManager.UserTokenProvider = value;
            }
        }

        public UserService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public async Task<OperationDetails> CreateAsync(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Regiter was complete succesfuly.", "");
            }
            else
            {
                return new OperationDetails(false, "User with this name already exist.", "Email");
            }
        }

        public async Task<UserDTO> FindAsync(string email, string password)
        {
            ApplicationUser user = await Database.UserManager.FindAsync(email, password);
            if (user != null)
            {
                UserDTO resultUser = new UserDTO();
                resultUser.Email = user.Email;
                resultUser.Id = user.Id;
                resultUser.Name = user.ClientProfile.Name;
                resultUser.Address = user.ClientProfile.Address;
                return resultUser;
            }
            return null;
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task<bool> IsEmailConfirmed(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
                return user.EmailConfirmed;
            else
                return false;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await CreateAsync(adminDto);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            var token = await Database.UserManager.GenerateEmailConfirmationTokenAsync(userId);
            return token;
        }

        public async Task SendEmailAsync(string userId, string subject, string message)
        {
            await Database.UserManager.SendEmailAsync(userId, subject, message);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void SetDefaultTokenProvider()
        {
            var provider = new DpapiDataProtectionProvider("VideoShare");
            Database.UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
                provider.Create("EmailConfirmation"));
        }

        public async Task<IdentityResult> ConfirmEmail(string userId,string code)
        {
            return await Database.UserManager.ConfirmEmailAsync(userId, code);
        }
    }
}