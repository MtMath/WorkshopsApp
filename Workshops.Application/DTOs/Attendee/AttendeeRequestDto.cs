
using System.ComponentModel.DataAnnotations;

namespace Workshops.Application.DTOs.Attendee;

public record struct AttendeeRequestDto
{
    [Required] public int WorkshopId { get; init; }
    [Required] public int CollaboratorId { get; init; }
    
    public string? Slug { get; set; }
}