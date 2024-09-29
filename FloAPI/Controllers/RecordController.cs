using FloAPI.Business.Interfaces.Services;
using FloAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FloAPI.Controllers;

[AuthenticationFilter]
public class RecordController : Controller
{
    private readonly IRecordService _recordService;

    public RecordController(IRecordService recordService)
    {
        _recordService = recordService;
    }
    
    [Route("/Records")]
    public async Task<IActionResult> GetRecords()
    {
        var response = await _recordService.GetRecordsAsync();
        if (response.Success)
            return Ok(response.Data);
        return BadRequest(response);
    }
    
    [HttpGet("/Records/{materialId:int}")]
    public async Task<IActionResult> GetRecordsByMaterials(int materialId)
    {
        var response = await _recordService.GetRecordsByMaterialIdAsync(materialId);
        if (response.Success)
            return Ok(response.Data);
        return BadRequest(response);
    }
}