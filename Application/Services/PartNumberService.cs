
using Application.Dtos;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class PartNumberService: IPartNumberService
    {
        private readonly IGenericRepository<Master> _repository;

        public PartNumberService(IGenericRepository<Master> repository)
        {
            _repository = repository;
        }

        public async Task<DashboardStatsDto> GetParentPartNumbersStatsAsync()
        {
            var query = _repository.GetQueryable();

            var statsByProcess = await query
                    .GroupBy(x => x.Operation)
                    .Select(g => new ProcessStatsDto
                    {
                        Name = g.Key ?? "Sin proceso",
                        NPartes = g.Count(),
                        Efficiency = g.Average(x => (double?)x.PzsHr) ?? 0.0
                    })
                    .ToListAsync();

            var totalPartNumber = await query
                        .Select(x => x.ParentPartNumber)
                        .Distinct()
                        .CountAsync();

            return new DashboardStatsDto
            {
                TotalPartNumber = totalPartNumber,
                StatsByProcess = statsByProcess
            };
        }
    }
}