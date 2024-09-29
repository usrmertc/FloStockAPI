using FloAPI.Business.Interfaces.Services;
using FloAPI.Business.Models;
using FloAPI.Filters;
using FloAPI.ViewModels.Barcode;
using Microsoft.AspNetCore.Mvc;

namespace FloAPI.Controllers;

[AuthenticationFilter]
public class BarcodeController : Controller
{
    private readonly IBarcodeService _barcodeService;
    private readonly IRecordService _recordService;
    private readonly IMaterialService _materialService;

    public BarcodeController(IBarcodeService barcodeService, IRecordService recordService, IMaterialService materialService)
    {
        _barcodeService = barcodeService;
        _recordService = recordService;
        _materialService = materialService;
    }
    
    [Route("/Barcodes")]
    public async Task<IActionResult> GetBarcodes()
    {
        var response = await _barcodeService.GetBarcodesAsync();
        if (response.Success)
            return Ok(response.Data);
        return BadRequest(response);
    }

    [HttpPost]
    [Route("/Barcode/")]
    public async Task<IActionResult> GetBarcode([FromBody] BarcodeGetAndConsumeViewModel model)
    {
        if (!(model.Value > 999_999_999_999_999 && model.Value < 10_000_000_000_000_000))
            return BadRequest("Invalid barcode value.");
        
        var response = await _barcodeService.GetByValueAsync(model.Value);
        if (response.Success)
            return Ok(response.Data);
        return BadRequest(response);
    }
    
    [HttpDelete("/Barcode/Delete/{id:int}")]
    public async Task<IActionResult> DeleteBarcode(int id)
    {
        var response = await _barcodeService.DeleteAsync(id);
        if (response.Success)
            return Ok(response.Data);
        return BadRequest(response);
    }
    
    [HttpPost]
    [Route("/Barcode/Add")]
    public async Task<IActionResult> AddBarcode([FromBody] BarcodeAddViewModel model)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var materialStatus = await _materialService.GetByIdAsync(model.MaterialId);

        if (materialStatus.Success)
        {
            var barcode = new Barcode(model.NumberOfDecrease, model.MaterialId);
            
            var response = await _barcodeService.AddAsync(barcode);
            if(response.Success)
                return Ok(response.Data);
            return BadRequest(response);
        } 
        return BadRequest("Material is not exist.");
    }
    
    [HttpPut]
    [Route("/Barcode/Update")]
    public async Task<IActionResult> UpdateBarcode([FromBody] BarcodeUpdateViewModel model)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var barcodeStatus = await _barcodeService.GetByIdAsync(model.Id);
        if (barcodeStatus.Success)
        {
            var materialStatus = await _materialService.GetByIdAsync(model.MaterialId);

            if (materialStatus.Success)
            {
                var barcode = new Barcode(model.Id,model.NumberOfDecrease, model.MaterialId);
            
                var response = await _barcodeService.UpdateAsync(barcode);
                if(response.Success)
                    return Ok(barcode);
                return BadRequest(response);
            } 
            return BadRequest("Material is not exist belong to this Id.");
        }
        return BadRequest("Barcode is not exist belong to this Id.");        
    }

    [HttpPost]
    [Route("/Barcode/Consume/")]
    public async Task<IActionResult> ConsumeBarcode([FromBody] BarcodeGetAndConsumeViewModel model)
    {
        if (!(model.Value > 999_999_999_999_999 && model.Value < 10_000_000_000_000_000))
            return BadRequest("Invalid barcode value.");

        var barcodeStatus = await _barcodeService.GetByValueAsync(model.Value);
        if (barcodeStatus.Success)
        {
            var materialStatus = await _materialService.GetByIdAsync(barcodeStatus.Data!.MaterialId);
            if (materialStatus.Success)
            {
                var material = materialStatus.Data;
                var newCount = material!.Count - barcodeStatus.Data.NumberOfDecrease;
                if(newCount < 0)
                    return BadRequest("Not enough material for decrase. Should be problem with material data or barcode data.");
                
                // You can check is new cont below threshold limit or not here.
                // And implement your alarm trigger to inside of API
                // Or you can implement outside of API
                // End of this method it returns true or false
                // If its true should below or equal to threshold limit
                
                material.Count = newCount;

                var record = new Record(barcodeStatus.Data.NumberOfDecrease, false, barcodeStatus.Data.MaterialId);
                var recordStatus = await _recordService.AddAsync(record);
                if(recordStatus.Success)
                {
                    var result = await _materialService.UpdateAsync(material);
                    if (result.Success)
                    {
                        if (newCount <= material.ThresholdLimit)
                            return Ok(true);
                        return Ok(false);
                    }
                    return BadRequest(result);
                }
                else
                    return BadRequest(recordStatus);
            }
            return BadRequest("Material is not exist belong to this Id.");
        }
        return BadRequest("Barcode is not exist belong to this Value.");
    }
}