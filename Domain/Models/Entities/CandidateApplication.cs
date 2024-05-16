using Domain.Models.AuditBaseEntity;

namespace Domain.Models.Entities;

public class CandidateApplication : BaseEntity
{
    public string? ApplicantId { get; set; }
    public Program? Program { get; set; }
    public string? ProgramId { get; set; }
    public virtual ICollection<Answer>? Answers { get; set; }
}
