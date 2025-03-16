using System.ComponentModel.DataAnnotations;
using Workshops.Application.Attributes;

namespace Workshops.Application.DTOs.Workshops;

public record struct WorkshopRequestDto
{
    [Required(ErrorMessage = "Name is required"), MaxLength(128, ErrorMessage = "Name cannot exceed 128 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Date is required"), DataType(DataType.DateTime)] 
    [FutureDate(ErrorMessage = "Realization date must be in the future")]
    public DateTime Date { get; set; }
    
    public int? Capacity { get; set; }
}