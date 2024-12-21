using Quiz.Data;

namespace Quiz.Appliaction.Services
{
    public interface IGameService
    {
        Task<QuestionDto?> GetQuestion(int category);
        Task<CheckAnswer?> CheckAnswer(Guid answerId, int category);
    }
}
