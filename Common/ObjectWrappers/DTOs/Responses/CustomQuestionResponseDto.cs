using Common.Enums;

namespace Common.ObjectWrappers.DTOs.Responses;

public record CustomQuestionResponseDto
    (string? id,
    QuestionType questionType,
    string? questionText,
    byte maxChoiceAllowed,
    ICollection<ChoiceResponseDto>? choices);
