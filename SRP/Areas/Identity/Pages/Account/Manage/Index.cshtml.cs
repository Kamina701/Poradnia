using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Application.Contracts.Infrastructure;
using Identity.Models;

namespace Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<SRPUser> _userManager;
        private readonly SignInManager<SRPUser> _signInManager;
        private readonly ICodeService _codeService;

        public IndexModel(
            UserManager<SRPUser> userManager,
            SignInManager<SRPUser> signInManager, ICodeService codeService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _codeService = codeService;
        }
        [Display(Name = "Numer konta")]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [Phone]
            [Display(Name = "Numer telefonu")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Kod SMS (Ważny przez 2 minuty).")]
            [Required(ErrorMessage = "Przepisz otrzymany kod SMS.")]
            public string ConfirmationCode { get; set; }

        }
        private async Task LoadAsync(SRPUser user)
        {
            Username = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Input = new InputModel
            {
                PhoneNumber = user.PhoneNumber
            };
            Email = user.Email;
            Phone = user.PhoneNumber;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber && _codeService.VerifyCode(user.UserName, Input.PhoneNumber, Input.ConfirmationCode))
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            else if (!_codeService.VerifyCode(user.UserName, Input.PhoneNumber, Input.ConfirmationCode))
            {
                StatusMessage = "Błąd! Kod SMS się nie zgadza.";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Zaktualizowano dane.";
            _codeService.ClearCache();
            return RedirectToPage();
        }
    }
}
