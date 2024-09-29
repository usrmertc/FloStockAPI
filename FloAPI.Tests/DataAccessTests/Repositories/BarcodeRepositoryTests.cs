using FloAPI.DataAccess.DataContexts;
using FloAPI.DataAccess.Entities;
using FloAPI.DataAccess.Repositories;
using FloAPI.Tests.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace FloAPI.Tests.DataAccessTests.Repositories
{
    public class BarcodeRepositoryTests
    {
        private readonly FloApiDataContext _floApiDataContext;

        public BarcodeRepositoryTests()
        {
            _floApiDataContext = ContextBuilder.Build();
        }

        [Fact]
        private async Task GetBarcodesMethodShouldReturnAllBarcodes()
        {
            BarcodeRepository barcodeRepository = new BarcodeRepository(_floApiDataContext);

            List<Barcode> barcodes = await barcodeRepository.GetBarcodes();

            Assert.NotNull(barcodes);
            Assert.Equal(6, barcodes.Count);
            Assert.Contains(barcodes, b => b.Value == 1000000000000000);
            Assert.Contains(barcodes, b => b.Value == 1000000000000001);
            Assert.Contains(barcodes, b => b.Value == 1000000000000002);
            Assert.Contains(barcodes, b => b.Value == 1000000000000003);
            Assert.Contains(barcodes, b => b.Value == 1000000000000004);
            Assert.Contains(barcodes, b => b.Value == 1000000000000005);
        }

        [Fact]
        private async Task AddMethodShouldAddBarcodeToDatabase()
        {
            BarcodeRepository barcodeRepository = new BarcodeRepository(_floApiDataContext);

            Barcode barcode = new Barcode { Value = 1000000000008, NumberOfDecrease = 1, MaterialId = 3 };

            await barcodeRepository.Add(barcode);

            var result = await _floApiDataContext.Barcodes.Where(b => b.Value == 1000000000008).FirstOrDefaultAsync();
            Assert.NotNull(result);
            Assert.Equal(1000000000008, result.Value);
        }

        [Fact]
        private async Task DeleteByIdMethodShouldDeleteBarcode()
        {
            BarcodeRepository barcodeRepostiry = new BarcodeRepository(_floApiDataContext);

            Barcode barcode = new Barcode { Value = 1000000000009, NumberOfDecrease = 15, MaterialId = 2 };

            // Adds email first for checking delete method
            await _floApiDataContext.Barcodes.AddAsync(barcode);
            await _floApiDataContext.SaveChangesAsync();


            await barcodeRepostiry.DeleteById(barcode.Id);
            await _floApiDataContext.SaveChangesAsync();

            var result = await _floApiDataContext.Barcodes.FindAsync(barcode.Id);
            Assert.Null(result);
        }

        [Fact]
        private async Task GetByValueMethodShouldReturnBarcodeByValue()
        {
            BarcodeRepository barcodeRepository = new BarcodeRepository(_floApiDataContext);

            var barcode = await barcodeRepository.GetByValue(1000000000000003);

            Assert.NotNull(barcode);
            Assert.Equal(4 , barcode.Id);
        }
        
        [Fact]
        private async Task GetByIdMethodShouldReturnBarcodeById()
        {
            BarcodeRepository barcodeRepository = new BarcodeRepository(_floApiDataContext);

            var barcode = await barcodeRepository.GetById(4);

            Assert.NotNull(barcode);
            Assert.Equal(1000000000000003 , barcode.Value);
        }

        [Fact]
        private async Task UpdateMethodShouldUpdateBarcode()
        {
            BarcodeRepository barcodeRepository = new BarcodeRepository(_floApiDataContext);

            var barcode = await _floApiDataContext.Barcodes.Where(e => e.Id == 5).FirstOrDefaultAsync();

            Assert.NotNull(barcode);

            barcode.Value = 1000000100000;
            await barcodeRepository.Update(barcode);

            var updatedBarcode = await _floApiDataContext.Barcodes.Where(e => e.Id == 5).FirstOrDefaultAsync();
            Assert.NotNull(updatedBarcode);
            Assert.Equal(barcode.Value, updatedBarcode.Value);
        }
    }
}
