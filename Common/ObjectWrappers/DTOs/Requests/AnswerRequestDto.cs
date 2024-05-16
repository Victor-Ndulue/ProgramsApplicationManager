namespace Common.ObjectWrappers.DTOs.Requests
{
    public record AnswerRequestDto(
        string questionId,
        ICollection<ChoiceRequestDto> choices);
}
