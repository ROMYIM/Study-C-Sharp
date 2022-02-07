using System;

namespace Infrastructure.Schedule.Logging
{
    public class ScheduleLogScope : IDisposable
    {
        private string _scopeName;

        public ScheduleLogScope(object scope = null)
        {
            _scopeName = scope?.ToString();
        }
        
        public void Dispose()
        {
            _scopeName = null;
        }

        public override string ToString()
        {
            return _scopeName;
        }
    }
}