using Quiz.Data;

namespace Quiz.Api.Services
{
    public interface IQuizService
    {
        Task<QuestionDto?> GetQuestion(int category);
        Task<CheckAnswer> CheckAnswer(Guid answerId, int category);
    }
}
