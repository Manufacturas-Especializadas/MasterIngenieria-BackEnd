using Application.Dtos;
using Application.Interfaces;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MasterEngineeringService : IMasterEngineeringService
    {
        private readonly IExcelMasterEngineeringReader _execelReader;
        private readonly IMasterRepository _repository;
        private readonly IConfiguration _configuration;

        public MasterEngineeringService(
            IExcelMasterEngineeringReader excelReader,
            IMasterRepository repository,
            IConfiguration configuration)
        {
            _execelReader = excelReader;
            _repository = repository;
            _configuration = configuration;
        }


        //public async Task<RefreshResultDto> RefreshAsync()
        //{
        //    var path = _configuration["ExcelSettings:MasterEngineeringPath"];

        //    var rows = await _execelReader.ReadAsync(path!);

        //    int inserted = 0;
        //    int updated = 0;

        //    foreach(var row in rows)
        //    {
        //        var existing = await _repository.GetByChildPartNumberAsync()
        //    }
        //}
    }
}