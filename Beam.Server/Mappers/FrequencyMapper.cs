namespace Beam.Server.Mappers
{
    public static class FrequencyMapper
    {
        public static Shared.Frequency ToShared(this Data.Frequency f)
        {
            return new Shared.Frequency()
            {
                Id = f.FrequencyId,
                Name = f.Name
            };
        }
        public static Data.Frequency ToData(this Shared.Frequency f)
        {
            return new Data.Frequency()
            {
                FrequencyId = f.Id,
                Name = f.Name
            };
        }
    }
}
