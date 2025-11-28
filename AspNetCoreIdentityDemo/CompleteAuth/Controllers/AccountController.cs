using System.ComponentModel.DataAnnotations;
using CompleteAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompleteAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl=null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl=null)
        {
            if(!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email,model.Password,model.Remember,lockoutOnFailure:true);

            if(result.Succeeded)
            {
                return LocalRedirect(returnUrl??"/");
            }
            if(result.IsLockedOut)
            {
                return View("LockOut");
            }

            ModelState.AddModelError(string.Empty,"Information Is wrong");
            return View(model);
        }

    }


    public class LoginViewModel
    {
        [Required]public string Email {get;set;}
        [Required, DataType(DataType.Password)] public string Password {get;set;}
        public bool Remember {get;set;}
        public string ReturnUrl {get;set;}
    }

    public class RegisterViewModel
    {
        [Required] public string Email { get; set; }
        [Required] public string FullName { get; set; }
        [Required, DataType(DataType.Password)] public string Password { get; set; }
        [Required, Compare("Password")] public string ConfirmPassword { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Country { get; set; }
    }

}