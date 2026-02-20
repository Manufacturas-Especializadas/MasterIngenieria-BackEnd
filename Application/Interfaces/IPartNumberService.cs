using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPartNumberService
    {
        Task<DashboardStatsDto> GetParentPartNumbersStatsAsync(
            string? parentPartNumber,
            string? childPartNumber,
            string? process
        );

        Task<DashboardStatsDto> GetChildPartNumbersStatsAsync(
            string? parentPartNumber,
            string? childPartNumber,
            string? process
        );

        Task<KpiStatsDto> GetKpiStatsAsync();
    }
}