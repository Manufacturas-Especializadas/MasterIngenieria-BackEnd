using Core.Entities;
using Core.Interfaces;
using Core.Models;
using EFCore.BulkExtensions;
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
            var topIds = await _context.Masters
                    .Where(m => m.Line == line && m.TCiclo.HasValue)
                    .GroupBy(m => m.ParentPartNumber)
                    .Select(g => new
                    {
                        Id = g.OrderByDescending(x => x.TCiclo).Select(x => x.Id).FirstOrDefault(),
                        MaxCiclo = g.Max(x => x.TCiclo)
                    })
                    .OrderByDescending(x => x.MaxCiclo)
                    .Take(top)
                    .Select(x => x.Id)
                    .ToListAsync();

                    var results = await _context.Masters
                        .Where(m => topIds.Contains(m.Id))
                        .ToListAsync();

            return results.OrderByDescending(m => m.TCiclo);
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

        public async Task<SyncResult> UpsertBulkAsync(List<Master> masters)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.TruncateAsync<Master>();

                    var bulkConfig = new BulkConfig
                    {
                        BatchSize = 5000,
                        PropertiesToExclude = new List<string> { "Id" },
                        SetOutputIdentity = false,
                        PreserveInsertOrder = false,
                        CalculateStats = false
                    };

                    await _context.BulkInsertAsync(masters, bulkConfig);

                    await transaction.CommitAsync();
                    return new SyncResult { Success = true, TotalRecords = masters.Count };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    var inner = ex.InnerException != null ? $" | Inner: {ex.InnerException.Message}" : "";
                    throw new Exception($"Error en BulkInsert: {ex.Message}{inner}", ex);
                }
            }
        }
    }
}