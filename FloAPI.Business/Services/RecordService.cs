using FloAPI.Business.Interfaces.Services;
using FloAPI.Business.Mappings;
using FloAPI.Business.Models;
using FloAPI.Business.Responses;
using FloAPI.DataAccess.Interfaces.Repositories;

namespace FloAPI.Business.Services;

public class RecordService : IRecordService
{
    private readonly IRecordRepository _recordRepository;
    
    public RecordService(IRecordRepository recordRepository)
    {
        _recordRepository = recordRepository;
    }
    
    public async Task<ServiceRespond<Record>> AddAsync(Record record)
    {
        try
        {
            await _recordRepository.Add(RecordMapping.ModelToEntity(record));
            return new ServiceRespond<Record>
            {
                Data = record,
                Success = true
            };
        }
        catch (Exception ex)
        {
            var response = new ServiceRespond<Record>
            {
                Success = false,
                Message = "Can't access database or there's no material with this Id.",
            };
            response.Errors!.Add(ex.Message);
            return response;
        }
    }

    public async Task<ServiceRespond<List<Record>>> GetRecordsAsync()
    {
        try
        {
            var records = await _recordRepository.GetRecords();
            
            return new ServiceRespond<List<Record>>
            {
                Data = records.Select(r => RecordMapping.EntityToModel(r)).ToList(),
                Success = true
            };
        }
        catch (Exception ex)
        {
            var response = new ServiceRespond<List<Record>>
            {
                Success = false,
                Message = "Can't access database.",
            };
            response.Errors!.Add(ex.Message);
            return response;
        }
    }

    public async Task<ServiceRespond<List<Record>>> GetRecordsByMaterialIdAsync(int materialId)
    {
        try
        {
            var records = await _recordRepository.GetRecordsByMaterial(materialId);
            return new ServiceRespond<List<Record>>
            {
                Data = records.Select(r => RecordMapping.EntityToModel(r)).ToList(),
                Success = true
            };
        }
        catch (Exception ex)
        {
            var response = new ServiceRespond<List<Record>>
            {
                Success = false,
                Message = "Can't access database.",
            };
            response.Errors!.Add(ex.Message);
            return response;
        }
    }
}