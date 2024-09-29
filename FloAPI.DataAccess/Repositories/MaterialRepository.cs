using FloAPI.DataAccess.DataContexts;
using FloAPI.DataAccess.Entities;
using FloAPI.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FloAPI.DataAccess.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly FloApiDataContext _floApiDataContext;
        public MaterialRepository(FloApiDataContext floApiDataContext)
        {
            _floApiDataContext = floApiDataContext;
        }

        public async Task Add(Material material)
        {
            await _floApiDataContext.Materials.AddAsync(material);
            await _floApiDataContext.SaveChangesAsync();
        }

        public async Task<Material> GetById(int id)
        {
            return await _floApiDataContext.Materials.FindAsync(id);
        }
        
        public async Task<Material> GetByName(string name)
        {
            return await _floApiDataContext.Materials.Where(m => m.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<Material>> GetMaterials()
        {
            return await _floApiDataContext.Materials.ToListAsync();
        }

        public async Task Update(Material material)
        {
            var existingMaterial = await _floApiDataContext.Materials
                .FirstOrDefaultAsync(m => m.Id == material.Id);

            if (existingMaterial != null)
            {
                existingMaterial.Name = material.Name;
                existingMaterial.Count = material.Count;
                existingMaterial.ThresholdLimit = material.ThresholdLimit;

                await _floApiDataContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Material not found");
            }
        }
    }
}
