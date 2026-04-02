using Core.Entities;
using Core.Models;
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

        Task<IEnumerable<int>> GetUniqueLinesAsync();

        Task<SyncResult> UpsertBulkAsync(List<Master> masters);
    }
}