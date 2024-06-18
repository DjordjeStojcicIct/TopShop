using TopShop.Application;

namespace TopShop.API.Jwt
{
    public class JwtActor : IApplicationActor
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public IEnumerable<int> AllowedUseCases { get; set; }
    }

    public class UnauthorizedActor : IApplicationActor
    {
        public int Id => 0;

        public string Email => "";

        public string Username => "unauthorized";

        public IEnumerable<int> AllowedUseCases => new List<int> { 1, 5, 8 };
    }
}
