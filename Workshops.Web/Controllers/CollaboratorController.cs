using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshops.Application.DTOs;
using Workshops.Application.DTOs.Collaborators;
using Workshops.Application.Services;
using Workshops.Domain.Entities;

namespace Workshops.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController, Route("v{api:apiVersion}/[controller]", Name = "Collaborators"), ApiVersion("1.0")]
    public class CollaboratorController(CollaboratorService collaboratorService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponseDto<List<CollaboratorEntity>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<GenericResponseDto<List<CollaboratorEntity>>>> Get()
        {
            
            var collaborators = await collaboratorService.GetCollaborators();
            
            var response = new GenericResponseDto<List<CollaboratorEntity>>
            {
                Data = collaborators,
                Message = "Collaborators retrieved successfully."
            };
            
            
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GenericResponseDto<CollaboratorEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenericResponseDto<CollaboratorEntity>>> Get(int id)
        {
            var collaborator = await collaboratorService.GetCollaboratorById(id);
            
            if (collaborator == null)
                return NotFound();
            
            var response = new GenericResponseDto<CollaboratorEntity>
            {
                Data = collaborator,
                Message = $"Collaborator with {id} retrieved successfully."
            };
            
            return Ok(response);
        }

        [HttpPost, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CollaboratorRequestDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var collaborator = await collaboratorService.InsertCollaboratorAsync(requestDto);
            var response = new GenericResponseDto<CollaboratorEntity>
            {
                Data = collaborator,
                Message = "Collaborator created successfully."
            };
            
            return Ok(response);
        }

        [HttpPut("{id:int}"), Authorize]
        [ProducesResponseType(typeof(GenericResponseDto<CollaboratorEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] CollaboratorRequestDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var updatedCollaborator = await collaboratorService.UpdateCollaboratorAsync(id, requestDto);
            
            var response = new GenericResponseDto<CollaboratorEntity>
            {
                Data = updatedCollaborator,
                Message = "Collaborator updated successfully."
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
                await collaboratorService.DeleteCollaborator(id);
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