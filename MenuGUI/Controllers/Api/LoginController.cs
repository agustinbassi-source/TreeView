using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MenuGUI.Controllers.Api
{
    public class LoginController : Controller
    {
        [HttpPost("/account/login")]
        public async Task<IActionResult> Login(UserCredentials credentials)
        {
            Boolean validatedUser = false;

            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, "corp.winsysgroup.com", "corp.winsysgroup.com\\serviceAcct", "serviceAcctPass"))
                {
                    validatedUser = context.ValidateCredentials(credentials.Username, credentials.Password);

                    if (validatedUser)
                    {

                        //Añadimos los claims Usuario y Rol para tenerlos disponibles en la Cookie
                        //Podríamos obtenerlos de una base de datos.
                        var claims = new[]
                        {
              new Claim(ClaimTypes.Name, credentials.Username)
            };

                        //Creamos el principal
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        //Generamos la cookie. SignInAsync es un método de extensión del contexto.
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);


                     

                        //Redirigimos a la Home
                        return LocalRedirect("/");

                    }
                    else
                    {
                        // return LocalRedirect("/login");
                    }
                }

            }
            catch (Exception ex)
            {
                //  return LocalRedirect("/login");
            }

            return Ok();
        }

        [HttpGet("/account/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

        [HttpGet("/account/getUser")]
        public async Task<IActionResult> GetUser()
        {
            try {

                string userName = User.Identities.FirstOrDefault().Name;

                if (string.IsNullOrEmpty(userName))
                {
                    return BadRequest(string.Empty);
                }

                return Ok(userName);

            }
            catch (Exception e)
            {
                return BadRequest(string.Empty);
            }
        }
    }
    public class UserCredentials
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
