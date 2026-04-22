using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class MasterImprovementDto
    {
        public string ParentPartNumber { get; set; } = null!;
        public decimal OldCycleTime { get; set; }

        public decimal NewCycleTime { get; set; }

        public decimal TimeSaved => OldCycleTime - NewCycleTime;

        public decimal PercentImprovement => OldCycleTime > 0
            ? Math.Round(((OldCycleTime - NewCycleTime) / OldCycleTime) * 100, 2)
            : 0;

        public string? Process { get; set; }

        public DateTime ImprovementDate { get; set; }
    }
}