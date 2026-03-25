using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class TopCycleTimeDto
    {
        public string PartNumber { get; set; }

        public string Description { get; set; }

        public decimal TCiclo { get; set; }

        public string Operation {  get; set; }
    }
}