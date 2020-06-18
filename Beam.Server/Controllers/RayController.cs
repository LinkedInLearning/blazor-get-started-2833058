using Beam.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Beam.Server.Mappers;
using Microsoft.EntityFrameworkCore;
using System;

namespace Beam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RayController : ControllerBase
    {
        Data.BeamContext _context;
        public RayController(Data.BeamContext context)
        {
            _context = context;
        }

        [HttpGet("user/{name}")]
        public List<Ray> GetRaysByUser(string name)
        {
            return _context.Rays.Include(r => r.Prisms).ThenInclude(p => p.User).Include(r => r.User)
                .Where(r => r.User.Username == name)
                .Select(r => r.ToShared()).ToList();
        }

        [HttpGet("user/{name}/{start}/{end}")]
        public List<Ray> GetRaysByUserAndDateRange(string name, DateTime start, DateTime end)
        {
            return _context.Rays.Include(r => r.Prisms).ThenInclude(p => p.User).Include(r => r.User)
                .Where(r => r.User.Username == name)
                .Where(r => r.CastDate > start && r.CastDate < end)
                .Select(r => r.ToShared()).ToList();
        }

        [HttpGet("userprisms/{name}")]
        public List<Ray> GetPrismedRaysByUser(string name)
        {
            return _context.Rays.Include(r => r.Prisms).ThenInclude(p => p.User).Include(r => r.User)
                .Where(r => r.Prisms.Any(p => p.User.Username == name))
                .Select(r => r.ToShared()).ToList();
        }

        [HttpGet("{FrequencyId}")]
        public List<Ray> Rays(int FrequencyId)
        {
            return GetRays(FrequencyId);
        }

        private List<Ray> GetRays(int FrequencyId)
        {
            return _context.Rays.Include(r => r.Prisms).ThenInclude(p => p.User).Include(r => r.User)
                .Where(r => r.FrequencyId == FrequencyId)
                .Select(r => r.ToShared()).ToList();
        }

        [HttpPost("[action]")]
        public List<Ray> Add([FromBody] Ray ray)
        {
            _context.Add(ray.ToData());
            _context.SaveChanges();
            return GetRays(ray.FrequencyId);
        }

    }
}