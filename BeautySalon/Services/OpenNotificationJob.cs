using Microsoft.Extensions.Logging;

namespace BeautySalon.Services
{
    public class OpenNotificationJob
    {
        public async Task Notificate()
        {
            Console.WriteLine( "Мы открылись!" );
        }
    }
}
