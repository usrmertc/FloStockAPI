using FloAPI.Business.Interfaces.Services;
using FloAPI.Business.Mappings;
using FloAPI.Business.Models;
using FloAPI.Business.Responses;
using FloAPI.DataAccess.Interfaces.Repositories;

namespace FloAPI.Business.Services;

public class BarcodeService : IBarcodeService
{
    private readonly IBarcodeRepository _barcodeRepository;
    
    public BarcodeService(IBarcodeRepository barcodeRepository)
    {
        _barcodeRepository = barcodeRepository;
    }

    public async Task<ServiceRespond<Barcode>> GetByValueAsync(Int64 value)
    {
        try
        {
            var barcode = await _barcodeRepository.GetByValue(value);
            return new ServiceRespond<Barcode>
            {
                Data = BarcodeMapping.EntityToModel(barcode),
                Success = true
            };
        }
        catch (Exception ex)
        {
            var response = new ServiceRespond<Barcode>
            {
                Success = false,
                Message = "Can't access database or Barcode is missing.",
            };
            response.Errors!.Add(ex.Message);
            return response;
        }
    }
    
    public async Task<ServiceRespond<Barcode>> GetByIdAsync(int barcodeId)
    {
        try
        {
            var barcode = await _barcodeRepository.GetById(barcodeId);
            return new ServiceRespond<Barcode>
            {
                Data = BarcodeMapping.EntityToModel(barcode),
                Success = true
            };
        }
        catch (Exception ex)
        {
            var response = new ServiceRespond<Barcode>
            {
                Success = false,
                Message = "Can't access database or Barcode is missing.",
            };
            response.Errors!.Add(ex.Message);
            return response;
        }
    }

    public async Task<ServiceRespond<List<Barcode>>> GetBarcodesAsync()
    {
        try
        {
            var barcodes = await _barcodeRepository.GetBarcodes();
            return new ServiceRespond<List<Barcode>>
            {
                Data = barcodes.Select(b => BarcodeMapping.EntityToModel(b)).ToList(),
                Success = true
            };
        }
        catch (Exception ex)
        {
            var response = new ServiceRespond<List<Barcode>>
            {
                Success = false,
                Message = "Can't access database.",
            };
            response.Errors!.Add(ex.Message);
            return response;
        }
    }

    public async Task<ServiceRespond<Barcode>> AddAsync(Barcode barcode)
    {
        try
        {
            barcode.Value = await GenerateUniqueBarcodeValue();
            await _barcodeRepository.Add(BarcodeMapping.ModelToEntity(barcode));
            return new ServiceRespond<Barcode>()
            {
                Data = barcode,
                Success = true
            };
        }
        catch (Exception ex)
        {
            var response = new ServiceRespond<Barcode>
            {
                Success = false,
                Message = "Can't access database or there's problem with your barcode model. Check your database maybe your barcode already exists.",
            };
            response.Errors!.Add(ex.Message);
            return response;
        }
    }

    public async Task<ServiceRespond<int>> DeleteAsync(int barcodeId)
    {
        try
        {
            await _barcodeRepository.DeleteById(barcodeId);
            return new ServiceRespond<int>()
            {
                Data = barcodeId,
                Success = true
            };
        }
        catch (Exception ex)
        {
            var response = new ServiceRespond<int>
            {
                Success = false,
                Message = "Can't access database or barcode is not exist.",
            };
            response.Errors!.Add(ex.Message);
            return response;
        }
    }

    public async Task<ServiceRespond<Barcode>> UpdateAsync(Barcode barcode)
    {
        try
        {
            await _barcodeRepository.Update(BarcodeMapping.ModelToEntityWithId(barcode));
            return new ServiceRespond<Barcode>
            {
                Data = barcode,
                Success = true
            };
        }
        catch (Exception ex)
        {
            var response = new ServiceRespond<Barcode>
            {
                Success = false,
                Message = "Can't access database or barcode is not exist.",
            };
            response.Errors!.Add(ex.Message);
            return response;
        }
    }
    
    public async Task<long> GenerateUniqueBarcodeValue()
    {
        long barcodeValue;
        bool isUnique = false;

        do
        {
            barcodeValue = GenerateRandom16DigitNumber();
                
            var existingBarcode = await _barcodeRepository.GetByValue(barcodeValue);

            if (existingBarcode == null)
            {
                isUnique = true;
            }
        }
        while (!isUnique);

        return barcodeValue;
    }

    private long GenerateRandom16DigitNumber()
    {
        var random = new Random();
        string result = string.Concat(Enumerable.Range(0, 16).Select(_ => random.Next(0, 10)));
        return long.Parse(result);
    }
}