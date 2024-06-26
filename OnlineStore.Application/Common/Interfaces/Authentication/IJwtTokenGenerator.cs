using OnlineStore.Domain.Models;

namespace OnlineStore.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(User user);
    }
}