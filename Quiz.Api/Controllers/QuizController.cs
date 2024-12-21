using Microsoft.AspNetCore.Mvc;
using Quiz.Api.Services;

namespace Quiz.Api.Controllers
{
    [ApiController]
    public class QuizController : ControllerBase
    {
        // https://localhost:7000/getquestion => Daj pytanie
        // https://localhost:7000/checkanswer => Sprawdü odpowiedü

        private readonly IQuizService _service;

        public QuizController(IQuizService service)
        {
            _service = service;
        }


        // Endpoint do generowania pytania z moøliwymi odpowiedziami
        // Przekazujemy kategorie

        [HttpGet]
        [Route("getquestion")]
        public async Task<IActionResult> GetQuestion(int category)
        {
            var question = await _service.GetQuestion(category);
            if (question == null)
                return BadRequest("Nieprawid≥owa kategoria pytania");
            else
                return Ok(question);    
        }



        // Endpoint do sprawdzania czy opowiedü gracza jest poprawna
        // Przekazujemy id odpowiedzi udzielonej przez gracza

        [HttpGet]
        [Route("checkanswer")]
        public async Task<IActionResult> CheckAnswer(Guid answerId, int category)
        {
            var result = await _service.CheckAnswer(answerId, category);
            return Ok(result);
        }
    }
}
