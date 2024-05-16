using Domain.Models.AuditBaseEntity;

namespace Domain.Models.Entities;

public class Choice : BaseEntity
{
    public string ChoiceText { get; set; }
    public CustomQuestion? Question { get; set; }
    public string? QuestionId { get; set; }
    public Answer? Answer { get; set; }
    public string? AnswerId { get; set; }
}
