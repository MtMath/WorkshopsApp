using System.ComponentModel.DataAnnotations;
using Workshops.Domain.Common;

namespace Workshops.Domain.Entities;

/// <summary>
/// Represents a workshop in the workshops system with its details and attendance records.
/// </summary>
public sealed class WorkshopsEntity : Entity
{
    /// <summary>
    /// Gets or sets the name of the workshop.
    /// </summary>
    [Required, MaxLength(128)] public required string Name { get; set; }
    /// <summary>
    /// Gets or sets the description of the workshop.
    /// </summary>
    [Required, MaxLength(255)] public required string Description { get; set; }
    /// <summary>
    /// Gets or sets the date when the workshop will be held.
    /// </summary>
    [Required] public required DateTime RealizationDate { get; set; }
    /// <summary>
    /// Slug for the workshop, human-readable and unique
    /// </summary>
    [Required, StringLength(50)] public required string Slug { get; set; }
    /// <summary>
    /// Maximum number of attendees
    /// </summary>
    public int? Capacity { get; set; }
    /// <summary>
    /// Gets or sets the attendees of the workshop.
    /// </summary>
    public ICollection<AttendeesRecordEntity> Attendees { get; set; }
}