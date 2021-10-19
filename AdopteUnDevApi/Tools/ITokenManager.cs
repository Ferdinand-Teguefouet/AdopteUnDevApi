using AdopteUnDevApi.Models;

namespace AdopteUnDevApi.Tools
{
    public interface ITokenManager
    {
        UserModel GenerateJWT(UserModel user);
    }
}