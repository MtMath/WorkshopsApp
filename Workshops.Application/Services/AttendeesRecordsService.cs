using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Workshops.Application.DTOs.Attendee;
using Workshops.Domain.Entities;
using Workshops.Domain.Interfaces;

namespace Workshops.Application.Services;

public interface IAttendeesRecordsService
{
}

public class AttendeesRecordsService(
    IRepository<AttendeesRecordEntity> recordRepository,
    ILogger<AttendeesRecordsService> logger)
    : IAttendeesRecordsService
{
    public async Task<List<AttendeesRecordEntity>> GetRecords()
    {
        try
        {
            return await recordRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching all attendance records.");
            throw;
        }
    }
    public async Task<AttendeesRecordEntity?> GetRecordById(int id)
    {
        try
        {
            if (id <= 0)
                throw new ArgumentException("Invalid record ID.");

            var record = await recordRepository.GetByIdAsync(id);
            if (record == null) logger.LogWarning("Record with ID {Id} not found.", id);

            return record;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching record with ID {Id}.", id);
            throw;
        }
    }
    public async Task<List<AttendeesRecordEntity>> GetRecordsByWorkshopId(int workshopId)
    {
        try
        {
            if (workshopId <= 0)
                throw new ArgumentException("Invalid workshop ID.");

            var records = await recordRepository.GetQueryable().Where(record => record.WorkshopId.Equals(workshopId))
                .ToListAsync();

            if (records.Count <= 0)
                logger.LogInformation("No attendance records found for workshop ID {WorkshopId}.", workshopId);

            return records;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching records for workshop ID {WorkshopId}.", workshopId);
            throw;
        }
    }
    public async Task<bool> DeleteRecordAsync(int id)
    {
        try
        {
            var record = await GetRecordById(id);

            if (record == null)
                throw new ArgumentException($"Record with ID {id} not found.");

            recordRepository.Delete(record);
            await recordRepository.SaveChangesAsync();

            logger.LogInformation("Attendance record with ID {Id} deleted.", id);

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting attendance record with ID {Id}.", id);
            throw;
        }
    }

    public async Task<AttendeesRecordEntity> AddCollaboratorToRecordAsync(AttendeeRequestDto requestDto)
    {
        try
        {
            if (requestDto.CollaboratorId <= 0)
                throw new ArgumentException("Invalid collaborator ID.");

            if (requestDto.WorkshopId <= 0)
                throw new ArgumentException("Invalid workshop ID.");
            
            var record = new AttendeesRecordEntity
            {
                CollaboratorId = requestDto.CollaboratorId,
                WorkshopId = requestDto.WorkshopId
            };
            
            var insertedRecord = await recordRepository.Insert(record);
            await recordRepository.SaveChangesAsync();

            return insertedRecord;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding collaborator {CollaboratorId} to workshop {WorkshopId}.",
                requestDto.CollaboratorId, requestDto.WorkshopId);
            throw;
        }
    }

    public async Task<bool> RemoveCollaboratorFromRecordAsync(AttendeeRequestDto requestDto)
    {
        try
        {
            var record = await recordRepository.GetQueryable().FirstOrDefaultAsync(
                r => r.CollaboratorId.Equals(requestDto.CollaboratorId)
                     && r.WorkshopId.Equals(requestDto.WorkshopId));

            if (record == null)
                throw new ArgumentException("Attendance record not found for this collaborator and workshop.");

            recordRepository.Delete(record);
            await recordRepository.SaveChangesAsync();

            logger.LogInformation("Collaborator {CollaboratorId} removed from workshop {WorkshopId}.",
                requestDto.CollaboratorId, requestDto.WorkshopId);

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error removing collaborator {CollaboratorId} from workshop {WorkshopId}.",
                requestDto.CollaboratorId, requestDto.WorkshopId);
            throw;
        }
    }

    [Obsolete("Not used in the current version.")]
    public async Task<bool> MarkAttendanceAsync(int recordId, bool attended)
    {
        try
        {
            var record = await GetRecordById(recordId);

            if (record == null)
                throw new ArgumentException($"Record with ID {recordId} not found.");

            record.Attended = attended;

            recordRepository.Update(record);
            await recordRepository.SaveChangesAsync();

            logger.LogInformation("Attendance for record ID {RecordId} updated to {AttendanceStatus}.",
                recordId, attended);

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error marking attendance for record ID {RecordId}.", recordId);
            throw;
        }
    }
}