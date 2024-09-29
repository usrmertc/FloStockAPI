using FloAPI.Business.Models;
using FloAPI.Business.Responses;

namespace FloAPI.Business.Interfaces.Services;

public interface IMaterialService
{
    Task<ServiceRespond<Material>> GetByIdAsync(int materialId);
    Task<ServiceRespond<Material>> GetByNameAsync(string materialName);
    Task<bool> IsMaterialNameUnique(string materialName);
    Task<ServiceRespond<List<Material>>> GetMaterialsAsync();
    Task<ServiceRespond<Material>> UpdateAsync(Material material);
    Task<ServiceRespond<Material>> AddAsync(Material material);
}