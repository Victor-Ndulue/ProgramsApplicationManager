namespace Common.ObjectWrappers.DTOs.Requests.Update;

public record ProgramUpdateDto
    (string id,
    string title,
    string description,
    ICollection<QuestionUpdateDto> questions,
    ICollection<CustomQuestionUpdateDto>? customQuestions
    );