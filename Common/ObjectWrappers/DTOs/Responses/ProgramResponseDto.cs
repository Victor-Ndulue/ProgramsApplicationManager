namespace Common.ObjectWrappers.DTOs.Responses;

public record ProgramResponseDto
    (string? id,
    string? title,
    string? description,

    ICollection<QuestionResponseDto>? questions,
    ICollection<CustomQuestionResponseDto>? customQuestions);