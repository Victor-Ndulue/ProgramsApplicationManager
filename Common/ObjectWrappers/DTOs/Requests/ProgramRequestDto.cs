namespace Common.ObjectWrappers.DTOs.Requests;

public class ProgramRequestDto
    (
    string title,
    string description,
    ICollection<QuestionRequestDto> questions,
    ICollection<CustomQuestionsRequestDto>? customQuestions,
    string? employerId);