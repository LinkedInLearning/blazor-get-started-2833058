using Beam.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Beam.Server.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Beam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrismController : ControllerBase
    {
        Data.BeamContext _context;
        public PrismController(Data.BeamContext context)
        {
            _context = context;
        }

        [HttpPost("[action]")]
        public List<Ray> Add([FromBody] Prism prism)
        {
            var newPrism = prism.ToData();

            _context.Add(newPrism);
            _context.SaveChanges();

            var prismRay = _context.Rays.Find(newPrism.RayId);

            return _context.Rays.Include(r => r.Prisms).ThenInclude(p => p.User).Include(r => r.User)
                .Where(r => r.FrequencyId == prismRay.FrequencyId)
                .Select(r => r.ToShared())
                .ToList();
        }

        [HttpGet("[action]/{UserId}/{RayId}")]
        public List<Ray> Remove(int UserId, int RayId)
        {
            var removePrisms = _context.Prisms.Include(p => p.Ray).Where(p => p.RayId == RayId && p.UserId == UserId).ToList();
            if (removePrisms == null || removePrisms.Count <= 0) return new List<Ray>();

            var frequencyId = removePrisms.First().Ray.FrequencyId;
            _context.RemoveRange(removePrisms);

            _context.SaveChanges();

            return _context.Rays.Include(r => r.Prisms).ThenInclude(p => p.User).Include(r => r.User)
                .Where(r => r.FrequencyId == frequencyId)
                .Select(r => r.ToShared())
                .ToList();
        }

    }
}