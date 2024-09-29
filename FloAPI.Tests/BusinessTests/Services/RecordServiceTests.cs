using FloAPI.Business.Services;
using FloAPI.DataAccess.DataContexts;
using FloAPI.DataAccess.Interfaces.Repositories;
using FloAPI.DataAccess.Repositories;
using FloAPI.Tests.DataContexts;
using Moq;
using Record = FloAPI.DataAccess.Entities.Record;

namespace FloAPI.Tests.BusinessTests.Services;

public class RecordServiceTests
{
    
    private readonly FloApiDataContext _floApiDataContext;
    private readonly RecordRepository _recordRepository;

    public RecordServiceTests()
    {
        _floApiDataContext = ContextBuilder.Build();
        _recordRepository = new RecordRepository(_floApiDataContext);
    }

    [Fact]
    private async Task GetRecordsMethodShouldReturnAllRecords()
    {
        RecordService recordService = new RecordService(_recordRepository);
        
        var response = await recordService.GetRecordsAsync();

        Assert.True(response.Success);
        Assert.NotNull(response.Data);
        Assert.Equal(_floApiDataContext.Records.Count(), response.Data.Count);
    }

    [Fact]
    private async Task GetRecordsMethodShouldReturnCantAccessDatabase()
    {
        var mockRepository = new Mock<IRecordRepository>();
        mockRepository.Setup(repo => repo.GetRecords())
            .ThrowsAsync(new Exception("Simulated database failure"));

        RecordService recordService = new RecordService(mockRepository.Object);
        
        var response = await recordService.GetRecordsAsync();
        
        Assert.False(response.Success);
        Assert.Equal("Can't access database.", response.Message);
        Assert.NotNull(response.Errors);
        Assert.Contains("Simulated database failure",
            response.Errors); 
    }

    [Fact]
    private async Task GetRecordsByMaterialIdMethodShouldReturnAllRecordsByMaterial()
    {
        int materialId = 1;
        RecordService recordService = new RecordService(_recordRepository);

        var response = await recordService.GetRecordsByMaterialIdAsync(materialId);

        Assert.True(response.Success);
        Assert.NotNull(response.Data);
        Assert.All(response.Data, 
            record => Assert.Equal(materialId, record.MaterialId));
    }

    [Fact]
    private async Task GetRecordsByMaterialIdMethodShouldReturnCantAccessDatabase()
    {
        var mockRepository = new Mock<IRecordRepository>();
        mockRepository.Setup(repo => repo.GetRecordsByMaterial(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Simulated database failure"));

        RecordService recordService = new RecordService(mockRepository.Object);

        var response = await recordService.GetRecordsByMaterialIdAsync(1);

        Assert.False(response.Success);
        Assert.Equal("Can't access database.", response.Message);
        Assert.NotNull(response.Errors);
        Assert.Contains("Simulated database failure", response.Errors); 
    }
    
    [Fact]
    private async Task AddMethodShouldAddNewRecord()
    {
        RecordService recordService = new RecordService(_recordRepository);
        Business.Models.Record record = new Business.Models.Record(
            count: 79,
            operationType: true,
            materialId: 1
        );
        
        var response = await recordService.AddAsync(record);
        var result = await recordService.GetRecordsByMaterialIdAsync(1);
        var data = result.Data;
        
        Assert.True(response.Success);
        Assert.NotNull(data);
        Assert.Equal(5, data.Count);
        Assert.Contains(data, r => r.Count == response.Data!.Count);
    }
    
    [Fact]
    private async Task AddMethodShouldCantAccessDatabase()
    {
        var mockRepository = new Mock<IRecordRepository>();
        mockRepository.Setup(repo => repo.Add(It.IsAny<Record>()))
            .ThrowsAsync(new Exception("Simulated database failure"));
        
        RecordService recordService = new RecordService(mockRepository.Object);
        Business.Models.Record record = new Business.Models.Record(
            count: 79,
            operationType: true,
            materialId: 1
        );
        
        var response = await recordService.AddAsync(record);
        
        Assert.False(response.Success);
        Assert.Equal("Can't access database or there's no material with this Id.", response.Message);
        Assert.NotNull(response.Errors);
        Assert.Contains("Simulated database failure", response.Errors); 
    }
}