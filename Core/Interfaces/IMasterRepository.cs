using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMasterRepository
    {
        Task<IEnumerable<Master>> GetTopCycleTimesByLineAsync(int line, int top = 5);
    }
}