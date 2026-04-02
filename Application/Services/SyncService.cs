using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SyncService : ISyncService
    {
        private readonly IMasterRepository _repository;
        private readonly string _nasPath = @"\\192.168.25.54\Ing Industrial\Master de Ingeniería.xlsx";

        public SyncService(IMasterRepository repository) 
        {
            _repository = repository;
        }

        public async Task<SyncResult> SyncMasterFromExcelAsync()
        {
            var timer = System.Diagnostics.Stopwatch.StartNew();
            var masters = new List<Master>();

            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using (var stream = File.Open(_nasPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        reader.Read();

                        while (reader.Read())
                        {
                            masters.Add(new Master
                            {
                                ParentPartNumber = reader.GetValue(0)?.ToString(),
                                ChildPartNumber = reader.GetValue(1)?.ToString(),
                                ProcessComments = reader.GetValue(2)?.ToString(),
                                ExternalDiameter = reader.GetValue(4)?.ToString(),
                                WallThickness = reader.GetValue(5)?.ToString(),
                                Development = reader.GetValue(6)?.ToString(),
                                Description = reader.GetValue(7)?.ToString(),
                                Type = reader.GetValue(8)?.ToString(),
                                Family = reader.GetValue(9)?.ToString(),
                                Client = reader.GetValue(10)?.ToString(),
                                Line = TryParseInt(reader.GetValue(11)),
                                PartOfPurchase = reader.GetValue(12)?.ToString(),
                                QuantityXQuantity = TryParseInt(reader.GetValue(13)),
                                Operation = reader.GetValue(14)?.ToString(),
                                Sequence = TryParseInt(reader.GetValue(15)) ?? 0,
                                MajorSetup = reader.GetValue(17)?.ToString(),
                                MinorSetup = reader.GetValue(18)?.ToString(),
                                OperSetup = TryParseDouble(reader.GetValue(19)),
                                TCiclo = TryParseDecimal(reader.GetValue(20)),
                                Oper = TryParseDecimal(reader.GetValue(21)),
                                PzsHr = TryParseInt(reader.GetValue(22)),
                                Verification = reader.GetValue(23)?.ToString()
                            });
                        }
                    }
                }

                var result = await _repository.UpsertBulkAsync(masters);

                timer.Stop();

                result.ExecutionTimeSeconds = timer.Elapsed.TotalSeconds;
                result.Message = $"Sincronización exitosa. Se procesaron {masters.Count} registros.";
                result.Success = true;

                return result;
            }
            catch (Exception ex)
            {
                timer.Stop();
                return new SyncResult
                {
                    Success = false,
                    TotalRecords = 0,
                    ExecutionTimeSeconds = timer.Elapsed.TotalSeconds,
                    Message = $"Error crítico en la sincronización: {ex.Message}"
                };
            }
        }

        private int? TryParseInt(object val) => int.TryParse(val?.ToString(), out int res) ? res : null;
        private decimal? TryParseDecimal(object val) => decimal.TryParse(val?.ToString(), out decimal res) ? res : null;
        private double? TryParseDouble(object val) => double.TryParse(val?.ToString(), out double res) ? res : null;

    }
}