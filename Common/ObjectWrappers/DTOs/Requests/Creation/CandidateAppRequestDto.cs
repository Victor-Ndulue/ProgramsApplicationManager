namespace Common.ObjectWrappers.DTOs.Requests.Creation;

public record CandidateAppRequestDto
    (
    string? applicantId,
    string programId,
    ICollection<AnswerRequestDto> answers
    );
