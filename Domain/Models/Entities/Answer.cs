using Domain.Models.AuditBaseEntity;

namespace Domain.Models.Entities;

public class Answer : BaseEntity
{
    public CandidateApplication? CandidateApplication { get; set; }
    public string? CandidateApplicationId { get; set; }
    public Question? Question { get; set; }
    public string QuestionId { get; set; }
    public CustomQuestion? CustomQuestion { get; set; }
    public string? CustomQuestionId { get; set; }
    public virtual ICollection<Choice>? Choices { get; set; }
}
