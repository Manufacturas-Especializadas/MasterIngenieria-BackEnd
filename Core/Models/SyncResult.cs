using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class SyncResult
    {
        public bool Success { get; set; }

        public int TotalRecords { get; set; }

        public string Message { get; set; } = string.Empty;

        public double ExecutionTimeSeconds { get; set; }

        public DateTime SyncDate { get; set; } = DateTime.Now;
    }
}