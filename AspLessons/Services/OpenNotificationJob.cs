using Microsoft.Extensions.Logging;

namespace AspLessons.Services
{
    public class OpenNotificationJob
    {
        public async Task Notificate()
        {
            Console.WriteLine( "Мы открылись!" );
        }
    }
}
