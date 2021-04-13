using Microsoft.Extensions.Logging;

namespace EfCore.Logging
{
    public class EfLoggerProvider : ILoggerProvider
    {


        public ILogger CreateLogger(string categoryName)
        {
            return new EfLogger(categoryName);
        }

        public void Dispose()
        {
            
        }
    }
}