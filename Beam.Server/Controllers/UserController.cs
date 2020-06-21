using Beam.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Beam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        Data.BeamContext _context;
        public UserController(Data.BeamContext context)
        {
            _context = context;
        }

        [HttpGet("[action]/{Username}")]
        public User Get()
        {
            var appUser = _context.AppUsers.FirstOrDefault(ap => ap.Username == User.Identity.Name);

            if (appUser != null)
                return new Beam.Shared.User {
                    IsAuthenticated = User.Identity.IsAuthenticated,
                    Name = User.Identity.Name,
                    Claims = User.Claims
                        .ToDictionary(c => c.Type, c => c.Value),
                    Id = appUser.UserId
                };
            else 
                return new Beam.Shared.User {
                    IsAuthenticated = false,
                    Name = "Anon",
                    Id = 1
                };
        }

    }
}