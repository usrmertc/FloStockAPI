using FloAPI.DataAccess.Entities;

namespace FloAPI.DataAccess.Interfaces.Repositories
{
    public interface IMaterialRepository
    {
        public Task<List<Material>> GetMaterials();
        public Task<Material> GetById(int id);
        public Task<Material> GetByName(string name);
        public Task Update(Material material);
        public Task Add(Material material);
    }
}
