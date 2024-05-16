using Domain.Models.AuditBaseEntity;

namespace Domain.Models.Entities;

public class Program : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<Question>? Questions { get; set; }
    public virtual ICollection<CustomQuestion>? CustomQuestions { get; set; }
    public virtual ICollection<CandidateApplication>? CandidateApplications { get; set; }
    public string? EmployerId { get; set; }
}
