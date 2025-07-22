namespace TrinityContinuum.Models.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    /*
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;
    public string CreatedBy { get; set; } = "System";
    public string UpdatedBy { get; set; } = "System";
    */
}
