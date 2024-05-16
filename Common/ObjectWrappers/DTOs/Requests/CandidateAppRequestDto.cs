namespace Common.ObjectWrappers.DTOs.Requests;

public record CandidateAppRequestDto
    (string applicantId,
    string programId,
    ICollection<AnswerRequestDto> answers
    );
