using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshops.Application.DTOs.Workshops;
using Workshops.Application.Services;

namespace Workshops.Web.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController, Route("v{version:apiVersion}/[controller]", Name = "Workshops"), ApiVersion("1.0")]
public class WorkshopsController(WorkshopsService workshopsService) : ControllerBase
{
    [HttpGet(Order = 1)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var workshops = await workshopsService.GetAllWorkshopsAsync();
        return Ok(workshops);
    }
    
    [HttpGet("{id:int}", Order = 2)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        return Ok("Workshop V1");
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] WorkshopRequestDto workshopRequest)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        
        return Ok("Workshop V1 created");
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateWorkshop([FromBody] WorkshopRequestDto workshopRequest)
    {
        return Ok("Workshop V1 updated");
    }
    
    [HttpDelete("{id:int}"), Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteWorkshop(int id)
    {
        return Ok("Workshop V1 deleted");
    }
}