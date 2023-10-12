using Cargo.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cargo.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppSettings _appSettings;
        public AuthController(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<IActionResult> Index(LoginUserViewModel loginUserViewModel)
        {
            ViewBag.AppName = _appSettings.ApplicationName;
            var getAllUrl=$"{_appSettings.ApiBaseUrl}/user";
            using var client=new HttpClient();
            var response= await client.GetFromJsonAsync<List<UserInfoViewModel>>(getAllUrl);
            var responseUser = response.FirstOrDefault(x => x.Email == loginUserViewModel.Email);

            var claims = new List<Claim>();

            claims.Add(new Claim("id", responseUser.Id.ToString()));
            claims.Add(new Claim("email",responseUser.Email));
            claims.Add(new Claim("firstName",responseUser.FirstName));
            claims.Add(new Claim("lastName",responseUser.LastName));

            var cliamIdentity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

            var autProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48))
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(cliamIdentity),autProperties);


            return RedirectToAction("Index","Cargo");

        }

     
        public IActionResult Login()
        {
          

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel loginUserViewModel)
        {
            var insertUrl=$"{_appSettings.ApiBaseUrl}/user";
            using var client=new HttpClient();
            var response = await client.PostAsJsonAsync
            (insertUrl,loginUserViewModel);

            if(response.IsSuccessStatusCode) {
             return RedirectToAction( "Index","Auth", loginUserViewModel);

            }
            else
            {
                return View( "Login",loginUserViewModel);


            };
           

            
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
