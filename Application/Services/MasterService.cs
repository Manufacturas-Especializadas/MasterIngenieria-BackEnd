using Application.Dtos;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MasterService
    {
        private readonly IMasterRepository _repository;

        public MasterService(IMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TopCycleTimeDto>> GetTopFiveByLine(int line)
        {
            var data = await _repository.GetTopCycleTimesByLineAsync(line, 5);

            return data.Select(m => new TopCycleTimeDto
            {
                PartNumber = m.ChildPartNumber ?? "N/A",
                Client = m.Client ?? "Sin cliente",
                Description = m.Description ?? "Sin descripción",
                TCiclo = m.TCiclo ?? 0,
                Operation = m.Operation ?? ""
            });
        }

        public async Task<IEnumerable<int>> GetLines()
        {
            return await _repository.GetUniqueLinesAsync();
        }

        public async Task<IEnumerable<MasterImprovementDto>> GetRecentImprovements(int line)
        {
            var data = await _repository.GetRecentImprovementsByLineAsync(line, 10);

            return data.Select(i => new MasterImprovementDto
            {
                ParentPartNumber = i.ParentPartNumber,
                OldCycleTime = i.OldCycleTime,
                NewCycleTime = i.NewCycleTime,
                Process = i.Process ?? "N/A",
                ImprovementDate = i.ImprovementDate,
            });
        }
    }
}