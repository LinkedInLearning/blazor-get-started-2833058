using System.Linq;
using System.Threading.Tasks;
using Beam.Data;
using Beam.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace Beam.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        Data.BeamContext _context;
        public AuthController(UserManager<AuthUser> userManager, SignInManager<AuthUser> signInManager, Data.BeamContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user == null) return BadRequest("User does not exist");

            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!singInResult.Succeeded) return BadRequest("Invalid password");

            await _signInManager.SignInAsync(user, true);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Login login)
        {
            if (_context.AppUsers.Any(u => u.Username == login.Username ))
            return BadRequest("User Already Exists");
            var user = new AuthUser();
            user.UserName = login.Username;
        
            user.AppUser = new Data.User() { Username = login.Username };

            var result = await _userManager.CreateAsync(user, login.Password);
            if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);
            return await Login(login);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {            
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}