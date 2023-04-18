using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Contracts.Infrastructure;
using Application.Contracts.Persistance;
using Domain.Entities;
using Domain.Entities.Autitables;
using Identity.Models;
using Application.Contracts.Identity;

namespace Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<SRPUser> _signInManager;
        private readonly UserManager<SRPUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IAsyncRepository<UserNotification> _usernotificationRepository;

        private readonly IAsyncRepository<Access> _accessRepository;
        private readonly IUsersService _usersService;

        public RegisterModel(UserManager<SRPUser> userManager,
            SignInManager<SRPUser> signInManager,
            ILogger<RegisterModel> logger,
            IAsyncRepository<UserNotification> usernotificationRepository,
            IAsyncRepository<Access> accessRepository, IUsersService usersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _usernotificationRepository = usernotificationRepository;
            _accessRepository = accessRepository;
            _usersService = usersService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public class InputModel
        {
            [Required(ErrorMessage = "Numer konta jest wymagany.")]
            [Display(Name = "Numer konta")]
            [RegularExpression("[a-zA-Z]{3}0\\d{5}", ErrorMessage = "Numer konta musi mieć format ABC000000, np. WAR012345")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Imię jest wymagane.")]
            [Display(Name = "Imię")]
            [RegularExpression(@"[AaĄąBbCcĆćDdEeĘęFfGgHhIiJjKkLlŁłMmNnŃńOoÓóPpRrSsŚśTtUuWwYyZzŹźŻż]*", ErrorMessage = "Imię może zawierać tylko litery.")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Nazwisko jest wymagane.")]
            [Display(Name = "Nazwisko")]
            [RegularExpression(@"[AaĄąBbCcĆćDdEeĘęFfGgHhIiJjKkLlŁłMmNnŃńOoÓóPpRrSsŚśTtUuWwYyZzŹźŻż]*", ErrorMessage = "Nazwisko może zawierać tylko litery.")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Hasło jest wymagane.")]
            [StringLength(100, ErrorMessage = "{0} musi mieć pomiędzy {2} a {1} znaków długości.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Hasło")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Potwierdź hasło")]
            [Compare("Password", ErrorMessage = "Hasła nie są identyczne.")]
            public string ConfirmPassword { get; set; }
        }



        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new SRPUser { UserName = Input.Username, FirstName = Input.FirstName, LastName = Input.LastName };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {

                    _logger.LogInformation("User created a new account with password.");


                    // Notify Admins about new user
                    var admins = await _userManager.GetUsersInRoleAsync("Admin");
                    var userRegisteredNotification = Notification.NewUserRegistered(user.NormalizedUserName);
                    foreach (var admin in admins)
                    {
                        var userCreatedUn = UserNotification.NewUserRegistered(userRegisteredNotification, admin.Id);

                        await _usernotificationRepository.AddAsync(userCreatedUn);
                    }

                    await _usersService.ConfirmUser(user.Id);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);

                }
            }

            return Page();
        }
    }
}
