using Common.ObjectWrappers.DTOs.Requests.Creation;

namespace Common.ObjectWrappers.DTOs.Requests.Update;

public record CandidateAppUpdateDto(
    string id,
    ICollection<AnswerRequestDto>? answers);