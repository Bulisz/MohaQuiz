using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.DataBase;

namespace MohaQuiz.Backend.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext _context;

    public QuizRepository(AppDbContext context)
    {
        _context = context;
    }
}
