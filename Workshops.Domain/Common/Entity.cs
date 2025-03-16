using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workshops.Domain.Common;

/// <summary>
///  Base class for standard entities in the workshops system.
/// </summary>
public abstract class Entity
{
    // This can easily be modified to be Entity<T> and public T id to support different key types.
    // Using non-generic integer types for simplicity
    [Key] public int Id { get; set; }
    /// <summary>
    /// The date and time the entity was created. (auditing)
    /// </summary>
    [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public DateTime CreatedAt { get; set; } = DateTime.Now;
    /// <summary>
    /// The date and time the entity was last updated. (auditing)
    /// </summary>
    [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)] public DateTime UpdatedAt { get; set; } = DateTime.Now;
}