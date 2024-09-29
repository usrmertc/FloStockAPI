using FloAPI.Business.Interfaces.Services;
using FloAPI.Business.Mappings;
using FloAPI.Business.Models;
using FloAPI.Business.Responses;
using FloAPI.DataAccess.Interfaces.Repositories;

namespace FloAPI.Business.Services;

public class MaterialService : IMaterialService
{
    private readonly IMaterialRepository _materialRepository;
    
    public MaterialService(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository;
    }
    
    public async Task<ServiceRespond<Material>> GetByIdAsync(int materialId)
    {
        try
        {
            var material = await _materialRepository.GetById(materialId);
            return new ServiceRespond<Material>
            {
                Data = MaterialMapping.EntityToModel(material),
                Success = true
            };
        }
        catch (Exception ex)
        {
            var response = new ServiceRespond<Material>
            {
                Success = false,
                Message = "Can't access database or Material is missing.",
            };
            response.Errors!.Add(ex.Message);
            return response;
        }
    }

    public async Task<ServiceRespond<Material>> GetByNameAsync(string materialName)
    {
        try
        {
            var material = await _materialRepository.GetByName(materialName);
            return new ServiceRespond<Material>
            {
                Success = true,
                Data = MaterialMapping.EntityToModel(material),
            };
        }
        catch (Exception ex)
        {
            var response = new ServiceRespond<Material>
            {
                Success = false,
                Message = "Can't access database or Material is missing.",
            };
            response.Errors!.Add(ex.Message);
            return response;
        }
    }

    public async Task<ServiceRespond<List<Material>>> GetMaterialsAsync()
    {
        try
        {
            var materials = await _materialRepository.GetMaterials();
            return new ServiceRespond<List<Material>>
            {
                Data = materials.Select(m => MaterialMapping.EntityToModel(m)).ToList(),
                Success = true
            };
        }
        catch (Exception ex)
        {
            var response = new ServiceRespond<List<Material>>
            {
                Success = false,
                Message = "Can't access database.",
            };
            response.Errors!.Add(ex.Message);
            return response;
        }
    }

    public async Task<ServiceRespond<Material>> UpdateAsync(Material material)
    {
        try
        {
            await _materialRepository.Update(MaterialMapping.ModelToEntityWithId(material));
            return new ServiceRespond<Material>()
            {
                Data = material,
                Success = true
            };
        }
        catch (Exception ex)
        {
            var response = new ServiceRespond<Material>
            {
                Success = false,
                Message = "Can't access database or there's problem with your material model.",
            };
            response.Errors!.Add(ex.Message);
            return response;
        }
    }

    public async Task<ServiceRespond<Material>> AddAsync(Material material)
    {
        try
        {
            await _materialRepository.Add(MaterialMapping.ModelToEntity(material));
            return new ServiceRespond<Material>()
            {
                Data = material,
                Success = true
            };
        }
        catch (Exception ex)
        {
            var response = new ServiceRespond<Material>
            {
                Success = false,
                Message = "Can't access database or there's problem with your material model. Check your database maybe your material already exists.",
            };
            response.Errors!.Add(ex.Message);
            return response;
        }
    }

    public async Task<bool> IsMaterialNameUnique(string materialName)
    {
        if (await _materialRepository.GetByName(materialName) == null)
            return true;
        return false;
    }
}