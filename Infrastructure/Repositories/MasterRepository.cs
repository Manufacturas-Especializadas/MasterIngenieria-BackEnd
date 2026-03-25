using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MasterRepository : IMasterRepository
    {
        private readonly ApplicationDbContext _context;

        public MasterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Master>> GetTopCycleTimesByLineAsync(int line, int top = 5)
        {
            return await _context.Masters
                .Where(m => m.Line == line && m.TCiclo.HasValue)
                .OrderByDescending(m => m.TCiclo)
                .ThenBy(m => m.ParentPartNumber)
                .Take(top)
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetUniqueLinesAsync()
        {
            return await _context.Masters
                    .Where(m => m.Line != null)
                    .Select(m => m.Line)
                    .Distinct()
                    .OrderBy(l => l)
                    .Select(l => l.Value)
                    .ToListAsync();
        }
    }
}