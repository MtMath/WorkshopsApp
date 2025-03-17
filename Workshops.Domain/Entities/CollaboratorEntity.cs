using System.ComponentModel.DataAnnotations;
using Workshops.Domain.Common;

namespace Workshops.Domain.Entities;

/// <summary>
/// Represents a collaborator in the workshops system, who can attend workshops.
/// </summary>
public class CollaboratorEntity : Entity
{
    /// <summary>
    /// Gets or sets the name of the collaborator.
    /// </summary>
    [Required, MaxLength(126)] public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the collection of attendance records associated with the collaborator.
    /// </summary>
    public virtual ICollection<AttendeesRecordEntity> Attendances { get; set; } = new List<AttendeesRecordEntity>();
}