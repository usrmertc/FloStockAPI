using FloAPI.DataAccess.DataContexts;
using FloAPI.DataAccess.Entities;
using FloAPI.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FloAPI.DataAccess.Repositories
{
    public class BarcodeRepository : IBarcodeRepository
    {
        private readonly FloApiDataContext _floApiDataContext;
        public BarcodeRepository(FloApiDataContext floApiDataContext)
        {
            _floApiDataContext = floApiDataContext;
        }

        public async Task Add(Barcode barcode)
        {
            await _floApiDataContext.Barcodes.AddAsync(barcode);
            await _floApiDataContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            Barcode barcode = await _floApiDataContext.Barcodes.FindAsync(id);
            if (barcode != null){
                _floApiDataContext.Barcodes.Remove(barcode);
                await _floApiDataContext.SaveChangesAsync();   
            }
            else
                throw new KeyNotFoundException("Barcode not found");
        }

        public async Task<List<Barcode>> GetBarcodes()
        {
            return await _floApiDataContext.Barcodes.ToListAsync();
        }

        public async Task<Barcode> GetByValue(Int64 value)
        {
            return await _floApiDataContext.Barcodes.Where(b => b.Value == value).FirstOrDefaultAsync();
        }
        
        public async Task<Barcode> GetById(int barcodeId)
        {
            return await _floApiDataContext.Barcodes.FirstOrDefaultAsync(b => b.Id == barcodeId);
        }

        public async Task Update(Barcode barcode)
        {
            var existingBarcode = await _floApiDataContext.Barcodes
                .FirstOrDefaultAsync(b => b.Id == barcode.Id);

            if (existingBarcode != null)
            {
                existingBarcode.NumberOfDecrease = barcode.NumberOfDecrease;
                existingBarcode.MaterialId = barcode.MaterialId;


                await _floApiDataContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Barcode not found");
            }
        }
    }
}
