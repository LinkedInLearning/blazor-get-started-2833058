using System.Linq;

namespace Beam.Server.Mappers
{
    public static class RayMapper
    {
        public static Shared.Ray ToShared(this Data.Ray r)
        {
            return new Shared.Ray()
            {
                RayId = r.RayId,
                Text = r.Text,
                FrequencyId = r.FrequencyId,
                PrismCount = r.Prisms.Count,
                UserId = r.UserId ?? 0,
                UserName = r.User?.Username ?? "[missing]",
                RayCastDate = r.CastDate,
                UsersPrismed = r.Prisms.Select(p => p.User.Username).ToList()
            };
        }
        public static Data.Ray ToData(this Shared.Ray r)
        {
            return new Data.Ray()
            {
                RayId = r.RayId,
                Text = r.Text,
                FrequencyId = r.FrequencyId,
                UserId = r.UserId,
            };
        }
    }
}
