using FloAPI.Business.Models;
using FloAPI.Business.Services;
using FloAPI.DataAccess.DataContexts;
using FloAPI.DataAccess.Interfaces.Repositories;
using FloAPI.DataAccess.Repositories;
using FloAPI.Tests.DataContexts;
using Moq;

namespace FloAPI.Tests.BusinessTests.Services;

public class MaterialServiceTests
{
    private readonly FloApiDataContext _floApiDataContext;
    private readonly MaterialRepository _materialRepository;
    
    public MaterialServiceTests()
    {
        _floApiDataContext = ContextBuilder.Build();
        _materialRepository = new MaterialRepository(_floApiDataContext);
    }
    
    [Fact]
    private async Task GetMaterialsMethodShouldReturnAllMaterials()
    {
        MaterialService materialService = new MaterialService(_materialRepository);
        
        var response = await materialService.GetMaterialsAsync();

        Assert.True(response.Success);
        Assert.NotNull(response.Data);
        Assert.Equal(_floApiDataContext.Materials.Count(), response.Data.Count);
    }
    

    [Fact]
    private async Task GetMaterialsMethodShouldReturnCantAccessDatabase()
    {
        var mockRepository = new Mock<IMaterialRepository>();
        mockRepository.Setup(repo => repo.GetMaterials())
            .ThrowsAsync(new Exception("Simulated database failure"));

        MaterialService materialService = new MaterialService(mockRepository.Object);
        
        var response = await materialService.GetMaterialsAsync();
        
        Assert.False(response.Success);
        Assert.Equal("Can't access database.", response.Message);
        Assert.NotNull(response.Errors);
        Assert.Contains("Simulated database failure",
            response.Errors); 
    }
    
    [Fact]
    private async Task GetByIdMethodShouldReturnMaterial()
    {
        MaterialService materialService = new MaterialService(_materialRepository);
        
        var response = await materialService.GetByIdAsync(1);

        Assert.True(response.Success);
        Assert.NotNull(response.Data);
        Assert.Equal(1, response.Data.Id);
        Assert.Equal("Leather", response.Data.Name);
        Assert.Equal(85, response.Data.Count);
        Assert.Equal(10, response.Data.ThresholdLimit);
    }
    

    [Fact]
    private async Task GetByIdMethodShouldReturnCantAccessDatabase()
    {
        var mockRepository = new Mock<IMaterialRepository>();
        mockRepository.Setup(repo => repo.GetById(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Simulated database failure"));

        MaterialService materialService = new MaterialService(mockRepository.Object);
        
        var response = await materialService.GetByIdAsync(1);
        
        Assert.False(response.Success);
        Assert.Equal("Can't access database or Material is missing.", response.Message);
        Assert.NotNull(response.Errors);
        Assert.Contains("Simulated database failure",
            response.Errors); 
    }
    
    [Fact]
    private async Task UpdateMethodShouldUpdateExistingMaterial()
    {
        MaterialService materialService = new MaterialService(_materialRepository);
        Material material = new Material(
            id:2,
            name: "TestMaterialNew",
            count: 55,
            thresholdLimit: 10
        );
        
        var response = await materialService.UpdateAsync(material);
        var result = await materialService.GetMaterialsAsync();
        var data = result.Data;
        
        Assert.True(response.Success);
        Assert.NotNull(data);
        Assert.Equal(5, data.Count);
        Assert.Contains(data, m => m.Name == "TestMaterialNew");
    }
    
    [Fact]
    private async Task UpdateMethodShouldCantAccessDatabase()
    {
        var mockRepository = new Mock<IMaterialRepository>();
        mockRepository.Setup(repo => repo.Update(It.IsAny<DataAccess.Entities.Material>()))
            .ThrowsAsync(new Exception("Simulated database failure"));

        MaterialService materialService = new MaterialService(mockRepository.Object);
        Material material = new Material(
            name: "TestMaterial",
            count: 55,
            thresholdLimit: 10
        );
        
        var response = await materialService.UpdateAsync(material);
        
        Assert.False(response.Success);
        Assert.Equal("Can't access database or there's problem with your material model.", response.Message);
        Assert.NotNull(response.Errors);
        Assert.Contains("Simulated database failure",
            response.Errors); 
    }
    
    [Fact]
    private async Task AddMethodShouldAddNewMaterial()
    {
        MaterialService materialService = new MaterialService(_materialRepository);
        Material material = new Material(
            name: "TestMaterial",
            count: 55,
            thresholdLimit: 10
        );
        
        var response = await materialService.AddAsync(material);
        var result = await materialService.GetMaterialsAsync();
        var data = result.Data;
        
        Assert.True(response.Success);
        Assert.NotNull(data);
        Assert.Equal(6, data.Count);
        Assert.Contains(data, m => m.Name == response.Data!.Name);
    }
    
    [Fact]
    private async Task AddMethodShouldCantAccessDatabase()
    {
        var mockRepository = new Mock<IMaterialRepository>();
        mockRepository.Setup(repo => repo.Add(It.IsAny<DataAccess.Entities.Material>()))
            .ThrowsAsync(new Exception("Simulated database failure"));

        MaterialService materialService = new MaterialService(mockRepository.Object);
        Material material = new Material(
            name: "TestMaterial",
            count: 55,
            thresholdLimit: 10
        );
        
        var response = await materialService.AddAsync(material);
        
        Assert.False(response.Success);
        Assert.Equal("Can't access database or there's problem with your material model. Check your database maybe your material already exists.", response.Message);
        Assert.NotNull(response.Errors);
        Assert.Contains("Simulated database failure",
            response.Errors); 
    }
    
    [Fact]
    private async Task GetByNameMethodShouldReturnMaterialByName()
    {
        MaterialService materialService = new MaterialService(_materialRepository);   
        var response = await materialService.GetByNameAsync("Rubber");
        Assert.True(response.Success);
        Assert.NotNull(response.Data);
        Assert.Equal(2, response.Data.Id);
        Assert.Equal("Rubber", response.Data.Name);
        Assert.Equal(40, response.Data.Count);
        Assert.Equal(10, response.Data.ThresholdLimit);
    }
    
    [Fact]
    private async Task GetByValueMethodShouldCantAccessDatabase()
    {
        var mockRepository = new Mock<IMaterialRepository>();
        mockRepository.Setup(repo => repo.GetByName(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Simulated database failure"));

        MaterialService materialService = new MaterialService(mockRepository.Object);
        
        var response = await materialService.GetByNameAsync("Rubber");
        
        Assert.False(response.Success);
        Assert.Equal("Can't access database or Material is missing.", response.Message);
        Assert.NotNull(response.Errors);
        Assert.Contains("Simulated database failure",
            response.Errors); 
    }

    [Fact]
    private async Task IsMaterialNameUniqueShouldReturnTrue()
    {
        MaterialService materialService = new MaterialService(_materialRepository);
        string materialName = "ShouldReturnTrue";
        Assert.True(await materialService.IsMaterialNameUnique(materialName));
    }
    
    [Fact]
    private async Task IsMaterialNameUniqueShouldReturnFalse()
    {
        MaterialService materialService = new MaterialService(_materialRepository);
        string materialName = "Rubber";
        Assert.False(await materialService.IsMaterialNameUnique(materialName));
    }
}