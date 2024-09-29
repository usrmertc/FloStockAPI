using FloAPI.Business.Models;
using FloAPI.Business.Responses;

namespace FloAPI.Business.Interfaces.Services;

public interface IRecordService
{
    Task<ServiceRespond<Record>> AddAsync(Record record);
    Task<ServiceRespond<List<Record>>> GetRecordsAsync();
    Task<ServiceRespond<List<Record>>> GetRecordsByMaterialIdAsync(int materialId);
}