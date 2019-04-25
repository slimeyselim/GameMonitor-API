using GameMonitor.Data.Entities;

namespace GameMonitor.Services
{
    public interface ITokenServices
    {
        Token GenerateToken(int userId);

        bool ValidateToken(string tokenId);

        bool Kill(string tokenId);

        bool DeleteByUserId(int userId);
    }
}
