namespace Common.ObjectWrappers.DTOs.Requests.Creation;

public record ProgramRequestDto
    (
    string title,
    string description,
    ICollection<QuestionRequestDto> questions,
    ICollection<CustomQuestionsRequestDto>? customQuestions,
    string? employerId);