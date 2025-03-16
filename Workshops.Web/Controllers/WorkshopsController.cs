using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshops.Application.DTOs;
using Workshops.Application.DTOs.Workshops;
using Workshops.Application.Services;
using Workshops.Domain.Entities;

namespace Workshops.Web.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController, Route("v{version:apiVersion}/[controller]", Name = "Workshops"), ApiVersion("1.0")]
public class WorkshopsController(WorkshopsService workshopsService) : ControllerBase
{
    [HttpGet(Order = 1), AllowAnonymous]
    [ProducesResponseType(typeof(GenericResponseDto<List<WorkshopsEntity>>), StatusCodes.Status200OK, "application/json")]
    public async Task<ActionResult<GenericResponseDto<List<WorkshopsEntity>>>> Get()
    {
        var workshops = await workshopsService.GetAllWorkshopsAsync();
        
        var response = new GenericResponseDto<List<WorkshopsEntity>>
        {
            Data = workshops,
            Message = "Workshops retrieved successfully.",
        };
        
        return Ok(response);
    }
    
    [HttpGet("{id:int}", Order = 2), AllowAnonymous]
    [ProducesResponseType(typeof(GenericResponseDto<WorkshopsEntity>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var workshop = await workshopsService.GetWorkshopByIdAsync(id);
        
        var response = new GenericResponseDto<WorkshopsEntity>
        {
            Data = workshop,
            Message = $"Workshop with {id} retrieved successfully.",
        };
        
        return Ok(response);
    }
    
    [HttpPost, Authorize]
    [ProducesResponseType(typeof(GenericResponseDto<WorkshopsEntity>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GenericResponseDto<WorkshopsEntity>>> Post([FromBody] WorkshopRequestDto workshopRequest)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        
        var workshop = await workshopsService.InsertWorkshopAsync(workshopRequest);
        
        var response = new GenericResponseDto<WorkshopsEntity>
        {
            Data = workshop,
            Message = "Workshop created successfully.",
        };
        
        return Ok(response);
    }
    
    [HttpPut("{id:int}"), Authorize]
    [ProducesResponseType(typeof(GenericResponseDto<WorkshopsEntity>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GenericResponseDto<WorkshopsEntity>>> Put(int id, [FromBody] WorkshopRequestDto workshopRequest)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        
        var workshop = await workshopsService.UpdateWorkshopAsync(id, workshopRequest);
        var response = new GenericResponseDto<WorkshopsEntity>
        {
            Data = workshop,
            Message = "Workshop updated successfully.",
        };
        
        return Ok(response);
    }
    
    [HttpDelete("{id:int}"), Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await workshopsService.DeleteWorkshopAsync(id);
        }
        catch (Exception)
        {
            //Adding this pattern to avoid returning 500 error,
            //Delete needs to be idempotent.
            //so we can return 404 if the resource is not found,
            //just to make it more clear or to confirm the resource is not there.
            return NotFound();
        }
        
        
        return NoContent();
    }
}