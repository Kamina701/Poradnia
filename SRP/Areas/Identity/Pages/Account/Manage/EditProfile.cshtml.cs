using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SRP.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SRP.Areas.Identity.Pages.Account.Manage
{
    public class EditProfileModel : PageModel
    {
        private readonly UserManager<SRPUser> _userManager;
        private readonly SignInManager<SRPUser> _signInManager;
        public EditProfileModel(
            UserManager<SRPUser> userManager,
            SignInManager<SRPUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
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
            [Display(Name = "Imiê")]
            public string FirstName { get; set; }
            [Display(Name = "Nazwisko")]
            public string LastName { get; set; }
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Display(Name = "Numer Telefonu")]
            public string Phone { get; set; }
        }
        private async Task LoadAsync(SRPUser user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (Input.FirstName == null && Input.LastName == null && Input.Email == null)
            {
                return RedirectToPage();
            }

            if (Input.FirstName != user.FirstName && Input.FirstName != null)
            {
                user.FirstName = Input.FirstName;
            }
            if (Input.LastName != user.LastName && Input.LastName != null)
            {
                user.LastName = Input.LastName;
            }
            if (Input.Email != user.Email && Input.Email != null)
            {
                user.Email = Input.Email;
            }
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Zaktualizowano dane.";
            return RedirectToPage();
        }
    }
}
