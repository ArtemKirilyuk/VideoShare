using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using VideoShare.PL.Models;
using VideoShare.BLL.DTO;
using System.Security.Claims;
using VideoShare.BLL.Interfaces;
using VideoShare.BLL.Infrastructure;
using System.Configuration;
using System.Text;
using Microsoft.AspNet.Identity;

namespace UserStore.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                
                if (claim == null)
                {
                    ModelState.AddModelError("", "Wrong login or password.");
                }
                else
                {
                    if (await UserService.IsEmailConfirmed(userDto))
                    {
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);
                    }
                    else ModelState.AddModelError("", "Email was not confirmed.");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegistrationViewModel model)
        {
            await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "user"
                };
                OperationDetails operationDetails = await UserService.CreateAsync(userDto);
                if (operationDetails.Succedeed)
                {
                    UserDTO userDtoWithId = await UserService.FindAsync(userDto.Email, userDto.Password);
                    string code = await UserService.GenerateEmailConfirmationTokenAsync(userDtoWithId.Id);
                    string callBackURL = Url.Action("ConfirmEmail", "Account", new { userId = userDtoWithId.Id, code = code },
                                                   protocol: Request.Url.Scheme);

                    StringBuilder confirmingMessage = new StringBuilder(ConfigurationManager.AppSettings["EmailConfirmingMessageStart"]);
                    confirmingMessage.Append("<a href=\"");
                    confirmingMessage.Append(callBackURL);
                    confirmingMessage.Append("\">Press to confirm email. <a/>");
                    confirmingMessage.Append(ConfigurationManager.AppSettings["EmailConfirmingMessageEnd"]);

                    string subject = ConfigurationManager.AppSettings["Subject"];
                    await UserService.SendEmailAsync(userDtoWithId.Id, subject, confirmingMessage.ToString());
                    return View("DisplayEmail");
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            IdentityResult operationResult = await UserService.ConfirmEmail(userId, code);
            if (operationResult.Succeeded)
            {
                return View("SuccessRegistration");
            }
            return View("Error");
        }

        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "somemail@mail.ru",
                UserName = "somemail@mail.ru",
                Password = "qwerty123",
                Name = "Семен Семенович Горбунков",
                Address = "ул. Спортивная, д.30, кв.75",
                Role = "admin",
            }, new System.Collections.Generic.List<string> { "user", "admin" });
        }
    }
}