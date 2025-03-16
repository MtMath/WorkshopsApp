using System.ComponentModel.DataAnnotations.Schema;
using Workshops.Domain.Common;

namespace Workshops.Domain.Entities;

/// <summary>
/// Represents the record of an attendee's participation in a workshop.
/// </summary>
public class AttendeesRecordEntity : Entity
{
    /// <summary>
    /// Gets or sets the foreign key for the associated workshop.
    /// </summary>
    [ForeignKey(nameof(WorkshopId))]
    public int WorkshopId { get; set; }

    /// <summary>
    /// Gets or sets the navigation property for the associated workshop.
    /// </summary>
    public WorkshopsEntity Workshop { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the associated collaborator.
    /// </summary>
    [ForeignKey(nameof(CollaboratorId))]
    public int CollaboratorId { get; set; }

    /// <summary>
    /// Gets or sets the navigation property for the associated collaborator.
    /// </summary>
    public CollaboratorEntity Collaborator { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the collaborator attended the workshop.
    /// </summary>
    public bool Attended { get; set; } = false;
}