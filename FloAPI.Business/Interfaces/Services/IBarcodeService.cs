using FloAPI.Business.Responses;
using FloAPI.Business.Models;

namespace FloAPI.Business.Interfaces.Services;

public interface IBarcodeService
{
    Task<ServiceRespond<Barcode>> GetByValueAsync(Int64 value);
    Task<ServiceRespond<Barcode>> GetByIdAsync(int barcodeId);
    Task<ServiceRespond<List<Barcode>>> GetBarcodesAsync();
    Task<ServiceRespond<Barcode>> AddAsync(Barcode barcode);
    Task<ServiceRespond<int>> DeleteAsync(int barcodeId);
    Task<ServiceRespond<Barcode>> UpdateAsync(Barcode barcode);
}