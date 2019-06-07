namespace Beto.Core.Web.Api
{
    using System;
    using System.Collections.Generic;

    public class LogDetailedModel
    {
        public LogDetailedModel()
        {
            this.Timestamp = DateTime.UtcNow;
        }

        public string Location { get; set; }

        public DateTime Timestamp { get; private set; }

        public string Message { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public Exception Exception { get; set; }

        public Dictionary<string, object> AdditionalInfo { get; set; }
    }
}