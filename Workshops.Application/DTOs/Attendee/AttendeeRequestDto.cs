
using System.ComponentModel.DataAnnotations;

namespace Workshops.Application.DTOs.Attendee;

public record struct AttendeeRequestDto
{
    [Required] public int Id { get; set; }
    
    [Required] public string Slug { get; set; }
}