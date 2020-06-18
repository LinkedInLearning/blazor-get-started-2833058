using Beam.Server.Mappers;
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
        public User Get(string Username)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == Username);

            if (existingUser != null) return existingUser.ToShared();

            var newUser = new Data.User() { Username = Username };

            _context.Add(newUser);
            _context.SaveChanges();

            return newUser.ToShared(); 
        }

    }
}