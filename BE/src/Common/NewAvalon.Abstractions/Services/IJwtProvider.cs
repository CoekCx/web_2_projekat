using NewAvalon.Abstractions.Contracts;

namespace NewAvalon.Abstractions.Services
{
    public interface IJwtProvider
    {
        string Generate(GenerateTokenRequest user);

        (string Email, string FirstName, string LastName) GetUserDetailsFromGoogleJwt(string googleToken);
    }
}
