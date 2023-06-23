using AutoMapper;
using MohaQuiz.Backend.Abstractions;

namespace MohaQuiz.Backend.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IMapper _mapper;

    public QuizService(IQuizRepository quizRepository, IMapper mapper)
    {
        _quizRepository = quizRepository;
        _mapper = mapper;
    }
}
