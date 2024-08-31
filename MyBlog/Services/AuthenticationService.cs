
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using MyBlog.Models;

namespace MyBlog.Services
{
    public class AuthenticationService
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public IEnumerable<IdentityUser> Users { get; set; }
        = Enumerable.Empty<IdentityUser>();

        public AuthenticationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(LoginViewModel model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }

        public async Task<SignInResult> LoginUserAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            return result;

        }

        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public IEnumerable<IdentityUser> GetUsers()
        {
            Users = _userManager.Users;

            foreach (var user in Users)
            {
                Console.WriteLine(user.UserName);
            }

            return Users;
        }

    }


}
