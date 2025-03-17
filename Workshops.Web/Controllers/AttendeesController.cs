using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshops.Application.DTOs;
using Workshops.Application.DTOs.Attendee;
using Workshops.Application.Services;
using Workshops.Domain.Entities;

namespace Workshops.Web.Controllers
{
    [ApiController, Route("v{api:apiVersion}/[Controller]", Name = "Attendees"), ApiVersion("1.0")]
    public class AttendeesController(AttendeesRecordsService recordsService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GenericResponseDto<List<AttendeesRecordEntity>>>> Get()
        {
            var records = await recordsService.GetRecords();
            
            var response = new GenericResponseDto<List<AttendeesRecordEntity>>
            {
                Data = records,
                Message = "Attendees retrieved successfully."
            };
            
            return Ok(response);
        }
        
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var record = await recordsService.GetRecordById(id);
            
            if (record == null)
                return NotFound();
            
            var response = new GenericResponseDto<AttendeesRecordEntity>
            {
                Data = record,
                Message = $"Record with {id} retrieved successfully."
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
                await recordsService.DeleteRecordAsync(id);
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
        
        [HttpPost("add-collaborator"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddCollaborator([FromBody] AttendeeRequestDto requestDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var record = await recordsService.AddCollaboratorToRecordAsync(requestDto);
            var response = new GenericResponseDto<AttendeesRecordEntity>
            {
                Data = record,
                Message = "Collaborator added to attendee successfully."
            };
            
            return Ok(response);
        }
        
        [HttpDelete("remove-collaborator"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveCollaborator([FromBody] AttendeeRequestDto requestDto)
        {
            try
            {
                await recordsService.RemoveCollaboratorFromRecordAsync(requestDto);
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
}
