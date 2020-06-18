namespace Beam.Server.Mappers
{
    public static class PrismMapper
    {
        public static Shared.Prism ToShared(this Data.Prism p)
        {
            return new Shared.Prism()
            {
                RayId = p.RayId,
                UserId = p.UserId,
                PrismDate = p.PrismDate
            };
        }
        public static Data.Prism ToData(this Shared.Prism p)
        {
            return new Data.Prism()
            {
                RayId = p.RayId,
                UserId = p.UserId
            };
        }
    }
}
