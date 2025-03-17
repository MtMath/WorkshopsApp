using Microsoft.Extensions.Logging;
using Workshops.Application.DTOs.Collaborators;
using Workshops.Domain.Entities;
using Workshops.Domain.Interfaces;

namespace Workshops.Application.Services;

public interface ICollaboratorService
{
}

public class CollaboratorService(IRepository<CollaboratorEntity> collaboratorRepository, ILogger<CollaboratorService> logger) : ICollaboratorService
{
    public async Task<List<CollaboratorEntity>> GetCollaborators()
    {
        return await collaboratorRepository.GetAllAsync();
    }
    
    /// <summary>
    /// Retrieves a collaborator by its ID.
    /// </summary>
    public async Task<CollaboratorEntity?> GetCollaboratorById(int id)
    {
        try
        {
            if (id <= 0)
                throw new ArgumentException("Invalid collaborator ID.");
            
            var collaborator = await collaboratorRepository.GetByIdAsync(id);
            if (collaborator == null) logger.LogWarning("Collaborator with ID {Id} not found.", id);

            return collaborator;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching collaborator with ID {Id}.", id);
            throw;
        }
    }
    
    public async Task<CollaboratorEntity> InsertCollaboratorAsync(CollaboratorRequestDto collaboratorDto)
    {
        var collaborator = new CollaboratorEntity
        {
            Name = collaboratorDto.Name
        };
        
        await collaboratorRepository.Insert(collaborator);
        await collaboratorRepository.SaveChangesAsync();
        
        return collaborator;
    }
    
    public async Task<CollaboratorEntity> UpdateCollaboratorAsync(int id, CollaboratorRequestDto collaboratorDto)
    {
        var collaborator = await GetCollaboratorById(id);
        
        if (collaborator == null)
            throw new ArgumentException("Collaborator not found.");
        
        collaborator.Name = collaboratorDto.Name;
        
        collaboratorRepository.Update(collaborator);
        await collaboratorRepository.SaveChangesAsync();
        
        return collaborator;
    }
    
    public async Task DeleteCollaborator(int id)
    {
        var collaborator = await GetCollaboratorById(id);
        if (collaborator == null)
            throw new ArgumentException("Collaborator not found.");

        collaboratorRepository.Delete(collaborator);
        await collaboratorRepository.SaveChangesAsync();
    }
}