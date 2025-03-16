using System.ComponentModel.DataAnnotations;
using Workshops.Domain.Common;

namespace WorkshopsApp.Domain.Entities;

/// <summary>
/// Represents a collaborator in the system, who can attend workshops.
/// </summary>
public sealed class CollaboratorEntity : Entity
{
    /// <summary>
    /// Gets or sets the name of the collaborator.
    /// </summary>
    [Required, MaxLength(126)] public required string Name { get; set; }

}