using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class DashboardStatsDto
    {
        public int TotalPartNumber { get; set; }

        public List<ProcessStatsDto> StatsByProcess { get; set; } = new();
    }
}