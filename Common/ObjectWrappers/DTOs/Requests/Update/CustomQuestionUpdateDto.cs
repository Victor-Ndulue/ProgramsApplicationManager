using Common.Enums;

namespace Common.ObjectWrappers.DTOs.Requests.Update;

public record CustomQuestionUpdateDto
(
    string id,
    QuestionType questionType,
    string questionText,
    byte maxChoiceAllowed,
    ICollection<ChoiceUpdateDto>? choices
);