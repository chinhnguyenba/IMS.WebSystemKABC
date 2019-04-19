using Microsoft.Extensions.Logging;
using System;

namespace IMS.WebSystemKingHub.Logger
{
    public class LogMessage
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Message { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}
