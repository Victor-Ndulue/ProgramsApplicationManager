namespace Common.ObjectWrappers.DTOs.Responses;

public record AnswerResponseDto
    (QuestionResponseDto question,
    string applicationId,
    CustomQuestionResponseDto customQuestion);
