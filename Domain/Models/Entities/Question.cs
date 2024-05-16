using Domain.Models.AuditBaseEntity;

namespace Domain.Models.Entities;

public class Question : BaseEntity
{
    public string? QuestionText { get; set; }
    public Program? Program { get; set; }
    public string? ProgramId { get; set; }
}
