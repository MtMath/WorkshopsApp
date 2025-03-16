using System.ComponentModel.DataAnnotations;

namespace Workshops.Application.DTOs.Collaborators;

public record struct CollaboratorRequestDto
{
    [Required(ErrorMessage = "Name is Required")] public string Name { get; set; }
}