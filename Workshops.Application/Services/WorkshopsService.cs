using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Workshops.Application.DTOs.Workshops;
using Workshops.Application.Utils;
using Workshops.Domain.Entities;
using Workshops.Domain.Interfaces;

namespace Workshops.Application.Services;

public interface IWorkshopsService
{
}

public class WorkshopsService(IRepository<WorkshopsEntity> workshopsRepository, ILogger<WorkshopsService> logger)
    : IWorkshopsService
{
    public async Task<List<WorkshopsEntity>> GetAllWorkshopsAsync()
    {
        return await workshopsRepository.GetAllAsync();
    }

    /// <summary>
    /// Retrieves a workshop by its ID.
    /// </summary>
    public async Task<WorkshopsEntity?> GetWorkshopByIdAsync(int id)
    {
        try
        {
            if (id <= 0)
                throw new ArgumentException("Invalid workshop ID.");

            var workshop = await workshopsRepository.GetByIdAsync(id);
            if (workshop == null) logger.LogWarning("Workshop with ID {Id} not found.", id);

            return workshop;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching workshop with ID {Id}.", id);
            throw;
        }
    }

    public async Task<WorkshopsEntity> InsertWorkshopAsync(WorkshopRequestDto workshopDto)
    {
        var slug = SlugHelper.GenerateSlug(workshopDto.Title);
        var uniqueSlug = slug;

        //Adds a counter to the slug if it already exists ( like nonce in RNG )
        var slugExists = await workshopsRepository.GetQueryable()
            .AnyAsync(w => w.Slug.Equals(slug));

        if (slugExists)
        {
            
            logger.LogWarning("Workshop with Slug {Slug} already exists.", slug);
            
            var counter = 1;
            do
            {
                uniqueSlug = $"{slug}-{counter}";
                counter++;
                slugExists = await workshopsRepository.GetQueryable()
                    .AnyAsync(w => w.Slug.Equals(uniqueSlug));
            } while (slugExists);
        }

        var workshop = new WorkshopsEntity
        {
            Name = workshopDto.Title,
            Description = workshopDto.Description,
            RealizationDate = workshopDto.Date,
            Capacity = workshopDto.Capacity ?? null,
            Slug = uniqueSlug,
        };

        await workshopsRepository.Insert(workshop);
        await workshopsRepository.SaveChangesAsync();
        
        //TODO: Send a command to generate attendance sheets for this workshop
        //Now I can generate Attendance Sheets for this workshop, since it was created successfully
        //This is a good moment (example) to use the MediatR library to send a command to another handler
        
        //TODO: For now Generate a AttendanceRecordService for simplicity

        return workshop;
    }
    
    public async Task<WorkshopsEntity> UpdateWorkshopAsync(int id, WorkshopRequestDto workshopDto)
    {
        var workshop = await GetWorkshopByIdAsync(id);
        if (workshop == null)
            throw new ArgumentException("Workshop not found.");

        //If the title is being updated, the slug must be updated as well, to keep the uniqueness of the slug
        //But if the title is the same, the slug remains the same. But the slug would not be updated for simplicity
        workshop.Name = workshopDto.Title;
        workshop.Description = workshopDto.Description;
        workshop.RealizationDate = workshopDto.Date;
        
        if (workshopDto.Capacity.HasValue)
            workshop.Capacity = workshopDto.Capacity.Value;

        workshopsRepository.Update(workshop);
        await workshopsRepository.SaveChangesAsync();

        return workshop;
    }
    
    public async Task DeleteWorkshopAsync(int id)
    {
        var workshop = await GetWorkshopByIdAsync(id);
        if (workshop == null)
            throw new ArgumentException("Workshop not found.");

        workshopsRepository.Delete(workshop);
        await workshopsRepository.SaveChangesAsync();
    }
}