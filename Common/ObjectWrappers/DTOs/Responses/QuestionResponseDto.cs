namespace Common.ObjectWrappers.DTOs.Responses;

public record QuestionResponseDto
{
    public string? id { get; init; }
    public string? questionText { get; init; }
}