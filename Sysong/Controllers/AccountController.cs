using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sysong.ViewModels;

namespace Sysong.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<IdentityUser> _userManager;
        readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View("Index", loginViewModel);

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                if (result.Succeeded)
                {
                    var userIsAdmin = await _userManager.IsInRoleAsync(user, "admin");

                    if (userIsAdmin)
                    {
                        return RedirectToAction("Index", "Home", new { area = "admin" });
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Nome do usuário ou senha inválidos!");
            return View("Index", loginViewModel);
        }
    }
}
