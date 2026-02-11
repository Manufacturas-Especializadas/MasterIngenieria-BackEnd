
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
                        NPartes = g.Select(x => x.ParentPartNumber).Distinct().Count(),
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

        public async Task<DashboardStatsDto> GetChildPartNumbersStatsAsync()
        {
            var query = _repository.GetQueryable();

            var statsByProcess = await query
                            .GroupBy(x => x.Operation)
                            .Select(g => new ProcessStatsDto
                            {
                                Name = g.Key ?? "Sin proceso",
                                NPartes = g.Select(x => x.ChildPartNumber).Distinct().Count(),
                                Efficiency = g.Average(x => (double?)x.PzsHr) ?? 0.0
                            })
                            .ToListAsync();

            var totalPartNumber = await query
                            .Select(x => x.ChildPartNumber)
                            .Distinct()
                            .CountAsync();

            return new DashboardStatsDto
            {
                TotalPartNumber = totalPartNumber,
                StatsByProcess = statsByProcess
            };
        }

        public async Task<KpiStatsDto> GetKpiStatsAsync()
        {
            var query = _repository.GetQueryable();

            var statsByProcess = await query
                        .GroupBy(x => x.Operation)
                        .Select(g => new ProcessStatsDto
                        {
                            Name = g.Key ?? "Sin proceso",
                            NPartes = g.Select(x => x.ParentPartNumber)
                                        .Union(g.Select(x => x.ChildPartNumber))
                                        .Distinct()
                                        .Count(),
                            Efficiency = g.Average(x => (double?)x.PzsHr) ?? 0.0
                        })
                        .ToListAsync();

            var totalUniqueParts = await query
                        .Select(x => x.ParentPartNumber)
                        .Union(query.Select(x => x.ChildPartNumber))
                        .Distinct()
                        .CountAsync();

            var topProcess = statsByProcess
                    .OrderByDescending(s => s.NPartes)
                    .FirstOrDefault();

            var totalProcessesCount = await query.CountAsync();

            return new KpiStatsDto
            {
                TotalUniqueParts = totalUniqueParts,
                MaxProcessLoad = topProcess?.NPartes ?? 0,
                MaxProcessName = topProcess?.Name ?? "N/A",
                TotalProcessesCount = totalProcessesCount,
                StatsByProcess = statsByProcess
            };
        }
    }
}