using Microsoft.AspNetCore.Mvc;
using MohaQuiz.Backend.Abstractions;

namespace MohaQuiz.Backend.Controllers;

[Route("mohaquiz/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }
}
