
using Ambev.Base.Domain.Validation;

namespace Ambev.Base.Domain.Entities;

/// <summary>
/// Entity base for all entities
/// </summary>
public class BaseEntity 
{

    public Guid Id { get; set; }

    /// <summary>
    /// Date was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date was delete logic
    /// </summary>
    public DateTime? InactivedAt { get; set; }    

    /// <summary>
    /// Register when update register
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public void SetCreated() => this.CreatedAt = DateTime.UtcNow;

    public void SetUpdate() => this.UpdatedAt = DateTime.UtcNow;

    public void SetInactive() => this.InactivedAt = DateTime.UtcNow;

    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }

    public int CompareTo(BaseEntity? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other!.Id.CompareTo(Id);
    }
}
