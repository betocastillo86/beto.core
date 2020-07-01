namespace Beto.Core.Data
{
    using System.Collections.Generic;

    public class BulkConfigCore
    {
        public BulkConfigCore()
        {
        }

        public List<string> UpdateByProperties { get; set; }

        public List<string> PropertiesToExclude { get; set; }

        public List<string> PropertiesToInclude { get; set; }

        public bool CalculateStats { get; set; }

        public bool WithHoldlock { get; set; }

        public bool TrackingEntities { get; set; }

        public bool UseTempDB { get; set; }

        public bool EnableStreaming { get; set; }

        public int? BulkCopyTimeout { get; set; }

        public int? NotifyAfter { get; set; }

        public int BatchSize { get; set; }

        public bool SetOutputIdentity { get; set; }

        public bool PreserveInsertOrder { get; set; }

        public bool UseOnlyDataTable { get; set; }
    }
}