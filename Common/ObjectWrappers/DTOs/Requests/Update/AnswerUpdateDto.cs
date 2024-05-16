namespace Common.ObjectWrappers.DTOs.Requests.Update;

public record AnswerUpdateDto(
    string id,
    ICollection<ChoiceUpdateDto> choices);
