using System.Threading.Tasks;
using GMS.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GMS.ASPNet.Core.Models.SessionViewModels;

namespace GMS.ASPNet.Core.Controllers
{
    /// <summary>
    /// Contains actions pertaining to user session management
    /// </summary>
    [Authorize]
    [Route("[controller]/[action]")]
    public class SessionController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger _logger;


        [TempData] public string ErrorMessage { get; set; }

        /// <summary>
        /// Dependency Injection is used to retrieve the services needed for us to manaage users
        /// </summary>
        /// <param name="userManager">ASP Net Identity User Manager</param>
        /// <param name="signInManager">ASP Net Identity Sign In Manager</param>
        /// <param name="logger">Default ASP Net Logger</param>
        public SessionController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ILogger<SessionController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Displays UI for logging in users
        /// </summary>
        /// <param name="returnUrl">Page to redirect to after user logs in</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// Attempts a login by using login data provided by the login page
        /// </summary>
        /// <param name="model">MVC model send using HTTP POST by the web page</param>
        /// <param name="returnUrl">Page to redirect to after user logs in</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            // Retry if data is invalid
            if (!ModelState.IsValid) return View(model);

            // Attempt sign in
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe,
                lockoutOnFailure: false);

            //Redirect after sign in
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return RedirectToLocal(returnUrl);
            }

            // Log sign in failure
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        /// <summary>
        /// Displays a Register a page
        /// </summary>
        /// <param name="returnUrl">Page to redirect to after user logs in</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        /// <summary>
        /// Called when Register view POSTs a new users data to the controller
        /// </summary>
        /// <param name="model">View model containin the new registrants datra</param>
        /// <param name="returnUrl">Page to redirect to after user logs in</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(model);

            // Creating the new user
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            // Sign new user in if regisration was succesful
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation("User created a new account with password.");
                return RedirectToLocal(returnUrl);
            }

            AddErrors(result);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


        // Helper method to check if returnURL is a local url
        // If not, redirects to home page
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}