using Common.Enums;

namespace Common.ObjectWrappers.DTOs.Requests.Creation;

public record CustomQuestionsRequestDto
    (
    QuestionType questionType,
    string questionText,
    byte maxChoiceAllowed,
    ICollection<ChoiceRequestDto>? choices
    );

