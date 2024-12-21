using Quiz.Data;
using System.Text.Json;

namespace Quiz.Appliaction.Services
{
    public class GameService : IGameService
    {
        private readonly HttpClient _client;
        private JsonSerializerOptions _serializerOptions;

        public GameService(HttpClient client)
        {
            _client = client;
            _serializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        }

        public async Task<CheckAnswer?> CheckAnswer(Guid answerId, int category)
        {
            var url = $"checkanswer?answerId={answerId}&category={category}";
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CheckAnswer>(data, _serializerOptions);
            }
            else
                return null;
        }

        public async Task<QuestionDto?> GetQuestion(int category)
        {
            var url = $"getQuestion?category={category}";
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<QuestionDto>(data, _serializerOptions);
            }
            else
                return null;
        }
    }
}
