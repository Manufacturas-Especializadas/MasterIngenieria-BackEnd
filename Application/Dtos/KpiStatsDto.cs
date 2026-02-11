using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class KpiStatsDto
    {
        public int TotalUniqueParts { get; set; }

        public int MaxProcessLoad { get; set; }

        public string MaxProcessName { get; set; } = string.Empty;

        public int TotalProcessesCount { get; set; }

        public List<ProcessStatsDto> StatsByProcess { get; set; } = new();
    }
}