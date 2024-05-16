namespace Common.ObjectWrappers.DTOs.Requests.Creation
{
    public record AnswerRequestDto(
        string questionId,
        ICollection<ChoiceRequestDto> choices);
}
