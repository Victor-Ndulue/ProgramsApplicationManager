namespace Common.ObjectWrappers.DTOs.Responses;

public record CandidateAppResponseDto
(string applicantId,
string programId,
ICollection<AnswerResponseDto> answers
);

