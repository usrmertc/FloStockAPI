using FloAPI.DataAccess.DataContexts;
using FloAPI.DataAccess.Entities;
using FloAPI.DataAccess.Repositories;
using FloAPI.Tests.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace FloAPI.Tests.DataAccessTests.Repositories
{
    public class MaterialRepositoryTests
    {
        private readonly FloApiDataContext _floApiDataContext;

        public MaterialRepositoryTests()
        {
            _floApiDataContext = ContextBuilder.Build();
        }

        [Fact]
        private async Task GetMaterialsMethodShouldReturnAllMaterialsAsList()
        {
            MaterialRepository materialRepository = new MaterialRepository(_floApiDataContext);

            List<Material> materials = await materialRepository.GetMaterials();

            Assert.NotNull(materials);
            Assert.Equal(5, materials.Count);
            Assert.Contains(materials, m => m.Name == "Leather");
            Assert.Contains(materials, m => m.Name == "Rubber");
            Assert.Contains(materials, m => m.Name == "Textiles");
            Assert.Contains(materials, m => m.Name == "Synthetics");
            Assert.Contains(materials, m => m.Name == "Foam");
        }

        [Fact]
        private async Task AddMethodShouldAddMaterial()
        {
            MaterialRepository materialRepository = new MaterialRepository(_floApiDataContext);

            Material material = new Material { Name = "Hide", Count = 25, ThresholdLimit = 10 };

            await materialRepository.Add(material);

            var result = await _floApiDataContext.Materials.Where(m => m.Name == "Hide").FirstOrDefaultAsync();
            Assert.NotNull(result);
            Assert.Equal(material.Name, result.Name);
            Assert.Equal(material.Count, result.Count);
            Assert.Equal(material.ThresholdLimit, result.ThresholdLimit);
        }

        [Fact]
        private async Task GetByIdMethodShouldGetMaterialById()
        {
            MaterialRepository materialRepository = new MaterialRepository(_floApiDataContext);

            var material = await materialRepository.GetById(2);

            Assert.NotNull(material);
            Assert.Equal(2, material.Id);
            Assert.Equal("Rubber", material.Name);
            Assert.Equal(40, material.Count);
            Assert.Equal(10, material.ThresholdLimit);
        }

        [Fact]
        private async Task GetByNameMethodShouldGetMaterialByName()
        {
            MaterialRepository materialRepository = new MaterialRepository(_floApiDataContext);

            var material = await materialRepository.GetByName("Rubber"); 
            Assert.NotNull(material);
            Assert.Equal(2, material.Id);
            Assert.Equal("Rubber", material.Name);
            Assert.Equal(40, material.Count);
            Assert.Equal(10, material.ThresholdLimit);
        }

        [Fact]
        private async Task UpdateMethodShouldUpdateMaterial()
        {
            MaterialRepository materialRepository = new MaterialRepository(_floApiDataContext);

            var material = await _floApiDataContext.Materials.Where(e => e.Id == 1).FirstOrDefaultAsync();

            Assert.NotNull(material);

            material.Count = 200;
            await materialRepository.Update(material);

            var updatedMaterial = await _floApiDataContext.Materials.Where(e => e.Id == 1).FirstOrDefaultAsync();
            Assert.NotNull(updatedMaterial);
            Assert.Equal("Leather", material.Name);
            Assert.Equal(200, material.Count);
            Assert.Equal(10, material.ThresholdLimit);
        }
    }
}
