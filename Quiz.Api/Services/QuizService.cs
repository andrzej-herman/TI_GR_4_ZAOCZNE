using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Extensibility;
using Quiz.Data;
using System.Data.Common;

namespace Quiz.Api.Services
{
    public class QuizService : IQuizService
    {
        private const string connStr = "Server=.\\HERMANLOCAL;Database=CqrsTp2;Integrated Security=true;TrustServerCertificate=true";
        private Random _random;
        private SqlConnection _connection;

        public QuizService()
        {
            _connection = new SqlConnection(connStr);
            _random = new Random();
        }


        public async Task<QuestionDto?> GetQuestion(int category)
        {
            var questions = new List<QuestionDto>();
            await _connection.OpenAsync();
            var query = $"select * from Questions where QuestionCategory = {category}";
            var command = new SqlCommand(query, _connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var qId = reader.GetGuid(0);
                var qCat = reader.GetInt32(1);
                var qCont = reader.GetString(2);
                questions.Add(new QuestionDto { Id = qId, Category = qCat, Content = qCont, Answers = [] });
            }
            await reader.CloseAsync();

            if (questions.Count == 0) return null;
            var index = _random.Next(0, questions.Count);
            var selectedQuestion = questions[index];
            var queryA = $"select AnswerId, AnswerContent from Answers where QuestionId = '{selectedQuestion.Id}'";
            var commandA = new SqlCommand(queryA, _connection);
            var readerA = commandA.ExecuteReader();
            while (readerA.Read())
            {
                var aId = readerA.GetGuid(0);
                var aCont = readerA.GetString(1);
                selectedQuestion.Answers.Add(new AnswerDto { Id = aId, Content = aCont });
            }

            await _connection.CloseAsync();
            return selectedQuestion;
        }

        public async Task<CheckAnswer> CheckAnswer(Guid answerId, int category)
        {
            List<int> categories = [100, 200, 300, 400, 500, 750, 1000];
            bool correct = false;
            await _connection.OpenAsync();
            var query = $"select AnswerIsCorrect from Answers where AnswerId = '{answerId}'";
            var command = new SqlCommand(query, _connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                correct = reader.GetBoolean(0);
            }
           
            int currentIndex = categories.IndexOf(category);
            var nextCategory = currentIndex == 6 ? 0 : categories[currentIndex + 1];

            await _connection.CloseAsync();
            return new CheckAnswer { IsCorrect = correct, NextCategory = nextCategory };
        }

        
    }
}


