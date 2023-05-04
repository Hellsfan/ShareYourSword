using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareYourSword.Models;
using System.Security.Claims;

namespace ShareYourSword.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginViewModel)
        {
            if (loginViewModel.UserName != null)
            {
                if (loginViewModel.UserName == "admin" && loginViewModel.Password == "asd1")
                {
                    var user = GetUserDetails(loginViewModel.UserName);

                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, loginViewModel.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.GivenName, ""));
                    identity.AddClaim(new Claim(ClaimTypes.Surname, ""));

                    foreach (var role in user.Roles)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role.name));
                    }

                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    loginViewModel.Message = "Incorrect username or password";
                }
            }

            return View(loginViewModel);
        }

        private UserModel GetUserDetails(string userName)
        {
            return new UserModel
            {
                FirstName = "George",
                LastName = "Petrov",
                Roles = new List<RoleModel> { new RoleModel() { name = "Admin" } }
            };
        }
    }
}
