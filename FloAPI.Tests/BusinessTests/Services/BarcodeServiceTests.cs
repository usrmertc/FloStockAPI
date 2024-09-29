using FloAPI.Business.Mappings;
using FloAPI.Business.Services;
using FloAPI.DataAccess.DataContexts;
using FloAPI.Business.Models;
using FloAPI.DataAccess.Interfaces.Repositories;
using FloAPI.DataAccess.Repositories;
using FloAPI.Tests.DataContexts;
using Moq;


namespace FloAPI.Tests.BusinessTests.Services;

public class BarcodeServiceTests
{
    private readonly FloApiDataContext _floApiDataContext;
    private readonly BarcodeRepository _barcodeRepository;
    
    public BarcodeServiceTests()
    {
        _floApiDataContext = ContextBuilder.Build();
        _barcodeRepository = new BarcodeRepository(_floApiDataContext);
    }
    
    [Fact]
    private async Task GetBarcodesMethodShouldReturnAllBarcodes()
    {
        BarcodeService barcodeService = new BarcodeService(_barcodeRepository);
        
        var response = await barcodeService.GetBarcodesAsync();

        Assert.True(response.Success);
        Assert.NotNull(response.Data);
        Assert.Equal(_floApiDataContext.Barcodes.Count(), response.Data.Count);
    }

    [Fact]
    private async Task GetBarcodesMethodShouldReturnCantAccessDatabase()
    {
        var mockRepository = new Mock<IBarcodeRepository>();
        mockRepository.Setup(repo => repo.GetBarcodes())
            .ThrowsAsync(new Exception("Simulated database failure"));

        BarcodeService barcodeService = new BarcodeService(mockRepository.Object);
        
        var response = await barcodeService.GetBarcodesAsync();
        
        Assert.False(response.Success);
        Assert.Equal("Can't access database.", response.Message);
        Assert.NotNull(response.Errors);
        Assert.Contains("Simulated database failure",
            response.Errors); 
    }

    [Fact]
    private async Task GetByValueMethodShouldReturnBarcodeByValue()
    {
        BarcodeService barcodeService = new BarcodeService(_barcodeRepository);
        
        var response = await barcodeService.GetByValueAsync(1_000_000_000_000_001);

        Assert.True(response.Success);
        Assert.NotNull(response.Data);
        Assert.Equal(2, response.Data.Id);
        Assert.Equal(1_000_000_000_000_001, response.Data.Value);
        Assert.Equal(1, response.Data.NumberOfDecrease);
        Assert.Equal(2, response.Data.MaterialId);
    }
    
    [Fact]
    private async Task GetByValueMethodShouldCantAccessDatabase()
    {
        var mockRepository = new Mock<IBarcodeRepository>();
        mockRepository.Setup(repo => repo.GetByValue(It.IsAny<Int64>()))
            .ThrowsAsync(new Exception("Simulated database failure"));

        BarcodeService barcodeService = new BarcodeService(mockRepository.Object);
        
        var response = await barcodeService.GetByValueAsync(1_000_000_000_000);
        
        Assert.False(response.Success);
        Assert.Equal("Can't access database or Barcode is missing.", response.Message);
        Assert.NotNull(response.Errors);
        Assert.Contains("Simulated database failure",
            response.Errors); 
    }
    
    [Fact]
    private async Task GetByIdMethodShouldReturnBarcodeById()
    {
        BarcodeService barcodeService = new BarcodeService(_barcodeRepository);
        
        var response = await barcodeService.GetByIdAsync(2);

        Assert.True(response.Success);
        Assert.NotNull(response.Data);
        Assert.Equal(2, response.Data.Id);
        Assert.Equal(1_000_000_000_000_001, response.Data.Value);
        Assert.Equal(1, response.Data.NumberOfDecrease);
        Assert.Equal(2, response.Data.MaterialId);
    }
    
    [Fact]
    private async Task GetByIdMethodShouldCantAccessDatabase()
    {
        var mockRepository = new Mock<IBarcodeRepository>();
        mockRepository.Setup(repo => repo.GetById(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Simulated database failure"));

        BarcodeService barcodeService = new BarcodeService(mockRepository.Object);
        
        var response = await barcodeService.GetByIdAsync(2);
        
        Assert.False(response.Success);
        Assert.Equal("Can't access database or Barcode is missing.", response.Message);
        Assert.NotNull(response.Errors);
        Assert.Contains("Simulated database failure",
            response.Errors); 
    }

    [Fact]
    private async Task AddMethodShouldAddNewBarcode()
    {
        BarcodeService barcodeService = new BarcodeService(_barcodeRepository);
        var barcode = new Barcode(
            numberOfDecrease: 55,
            materialId: 5
        );
        
        var response = await barcodeService.AddAsync(barcode);
        var result = await barcodeService.GetBarcodesAsync();
        var data = result.Data;
        
        Assert.True(response.Success);
        Assert.NotNull(data);
        Assert.Equal(7, data.Count);
        Assert.Contains(data, b => b.Value == response.Data!.Value);
    }

    [Fact]
    private async Task AddMethodShouldCantAccessDatabase()
    {
        var mockRepository = new Mock<IBarcodeRepository>();
        mockRepository.Setup(repo => repo.Add(It.IsAny<DataAccess.Entities.Barcode>()))
            .ThrowsAsync(new Exception("Simulated database failure"));

        BarcodeService barcodeService = new BarcodeService(mockRepository.Object);
        var barcode = new Barcode(
            numberOfDecrease: 55,
            materialId: 5
        );
        
        var response = await barcodeService.AddAsync(barcode);
        
        Assert.False(response.Success);
        Assert.Equal("Can't access database or there's problem with your barcode model. Check your database maybe your barcode already exists.", response.Message);
        Assert.NotNull(response.Errors);
        Assert.Contains("Simulated database failure",
            response.Errors); 
    }

    [Fact]
    private async Task DeleteShouldDeleteBarcode()
    {
        BarcodeService barcodeService = new BarcodeService(_barcodeRepository);
        
        var response = await barcodeService.DeleteAsync(3);
        var result = await barcodeService.GetBarcodesAsync();
        var data = result.Data;
        
        Assert.True(response.Success);
        Assert.Null(await _floApiDataContext.Barcodes.FindAsync(3));
        Assert.Equal(5, data!.Count);
    }
    
    [Fact]
    private async Task DeleteMethodShouldCantAccessDatabase()
    {
        var mockRepository = new Mock<IBarcodeRepository>();
        mockRepository.Setup(repo => repo.DeleteById(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Simulated database failure"));

        BarcodeService barcodeService = new BarcodeService(mockRepository.Object);
        
        var response = await barcodeService.DeleteAsync(2);
        
        Assert.False(response.Success);
        Assert.Equal("Can't access database or barcode is not exist.", response.Message);
        Assert.NotNull(response.Errors);
        Assert.Contains("Simulated database failure",
            response.Errors); 
    }
    
    [Fact]
    private async Task UpdateMethodShouldUpdateBarcode()
    {
        BarcodeService barcodeService = new BarcodeService(_barcodeRepository);
        var barcode = await _floApiDataContext.Barcodes.FindAsync(3);
        barcode!.NumberOfDecrease = 10;
        
        var response = await barcodeService.UpdateAsync(BarcodeMapping.EntityToModel(barcode));
        var result = await barcodeService.GetBarcodesAsync();
        var data = result.Data;
        var newBarcode = await _floApiDataContext.Barcodes.FindAsync(3);
        
        Assert.True(response.Success);
        Assert.NotNull(data);
        Assert.Equal(6, data.Count);
        Assert.Equal(10, newBarcode!.NumberOfDecrease);
    }
    
    [Fact]
    private async Task UpdateMethodShouldCantAccessDatabase()
    {
        var mockRepository = new Mock<IBarcodeRepository>();
        mockRepository.Setup(repo => repo.Update(It.IsAny<DataAccess.Entities.Barcode>()))
            .ThrowsAsync(new Exception("Simulated database failure"));

        BarcodeService barcodeService = new BarcodeService(mockRepository.Object);
        var barcode = new Barcode(
            numberOfDecrease: 55,
            materialId: 5
        );
        
        var response = await barcodeService.UpdateAsync(barcode);
        
        Assert.False(response.Success);
        Assert.Equal("Can't access database or barcode is not exist.", response.Message);
        Assert.NotNull(response.Errors);
        Assert.Contains("Simulated database failure",
            response.Errors); 
    }

    [Fact]
    private async Task GenerateUniqueBarcodeValueShouldReturnUniqueBarcodeValue()
    {
        BarcodeService barcodeService = new BarcodeService(_barcodeRepository);

        var value = await barcodeService.GenerateUniqueBarcodeValue();
        
        Assert.Null(_floApiDataContext.Barcodes.FirstOrDefault(b => b.Value == value));
        Assert.IsType<Int64>(value);
        Assert.True(value > 999_999_999_999_999 && value < 10_000_000_000_000_000);
    }
}