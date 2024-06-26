using OnlineStore.Domain.Models;

namespace OnlineStore.Application.Services.Authentication
{
    public record AuthenticationResult
    (
        User user,
        string Token
    );
}