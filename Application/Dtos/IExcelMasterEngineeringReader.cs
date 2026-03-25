using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public interface IExcelMasterEngineeringReader
    {
        Task<List<MasterEngineeringDto>> ReadAsync(string path);
    }
}