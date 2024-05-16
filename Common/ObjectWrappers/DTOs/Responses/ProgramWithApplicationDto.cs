namespace Common.ObjectWrappers.DTOs.Responses;

public record ProgramWithApplicationDto
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? programId { get; init; }
    public ICollection<QuestionResponseDto>? questions { get; init; }
    public ICollection<CustomQuestionResponseDto>? customQuestions { get; init; }
    public ICollection<AnswerResponseDto>? answers { get; init; }

}