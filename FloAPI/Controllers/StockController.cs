using FloAPI.Business.Interfaces.Services;
using FloAPI.Business.Models;
using FloAPI.Filters;
using FloAPI.ViewModels.Material;
using Microsoft.AspNetCore.Mvc;


namespace FloAPI.Controllers;

[AuthenticationFilter]
public class StockController : Controller
{
    private readonly IMaterialService _materialSerivce;
    private readonly IRecordService _recordService;

    public StockController(IMaterialService materialService, IRecordService recordService)
    {
        _materialSerivce = materialService;
        _recordService = recordService;
    }

    [Route("/Materials")]
    public async Task<IActionResult> GetMaterials()
    {
        var response = await _materialSerivce.GetMaterialsAsync();
        if (response.Success)
            return Ok(response.Data);
        return BadRequest(response);
    }
    
    [HttpGet("/Material/{id:int}")]
    public async Task<IActionResult> GetMaterial(int id)
    {
        var response = await _materialSerivce.GetByIdAsync(id);
        if (response.Success)
            return Ok(response.Data);
        return BadRequest(response);
    }
    
    [HttpPost("/Material")]
    public async Task<IActionResult> GetMaterial([FromBody] MaterialGetViewModel model)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var response = await _materialSerivce.GetByNameAsync(model.Name);
        if (response.Success)
            return Ok(response.Data);
        return BadRequest(response);
    }
    
    [HttpPost]
    [Route("/Material/Add")]
    public async Task<IActionResult> Add([FromBody] MaterialAddViewModel model)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!(await _materialSerivce.IsMaterialNameUnique(model.Name)))
            return BadRequest("Material name is already exist. Should be unique.");
        
        var material = new Material(model.Name, model.Count, model.ThresholdLimit);
        
        var response = await _materialSerivce.AddAsync(material);
        if (response.Success)
        {
            var isMaterialCreated = await _materialSerivce.GetByNameAsync(material.Name);
            if (isMaterialCreated.Success)
            {
                var record = new Record(material.Count, true, isMaterialCreated.Data!.Id);
                var isSuccess = await _recordService.AddAsync(record);
                if (isSuccess.Success)
                    return Ok(isMaterialCreated.Data);
                return BadRequest(isSuccess);
            }
            return BadRequest(isMaterialCreated.Message);
        }
        return BadRequest(response);
    }
}