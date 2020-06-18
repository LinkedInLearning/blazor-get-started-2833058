namespace Beam.Server.Mappers
{
    public static class UserMapper
    {
        public static Shared.User ToShared(this Data.User u)
        {
            return new Shared.User()
            {
                Id = u.UserId,
                Name = u.Username
            };
        }
        public static Data.User ToData(this Shared.User u)
        {
            return new Data.User()
            {
                UserId = u.Id,
                Username = u.Name
            };
        }
    }
}
