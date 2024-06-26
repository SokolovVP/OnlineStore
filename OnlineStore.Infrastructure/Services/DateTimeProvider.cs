using OnlineStore.Application.Common.Interfaces.Services;

namespace OnlineStore.Infrastructure.Services
{
    public class DateTimeProvider: IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}