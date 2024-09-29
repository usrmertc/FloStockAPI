using FloAPI.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloAPI.DataAccess.Interfaces.Repositories
{
    public interface IBarcodeRepository
    {
        public Task<List<Barcode>> GetBarcodes();
        public Task<Barcode> GetByValue(Int64 value);
        public Task<Barcode> GetById(int barcodeId);
        public Task DeleteById(int id);
        public Task Update(Barcode barcode);
        public Task Add(Barcode barcode);
    }
}
