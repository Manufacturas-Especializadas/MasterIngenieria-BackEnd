using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ProcessStatsDto
    {
        public string Name { get; set; }

        public int Partes { get; set; }

        public double Efficiency { get; set; }
    }
}