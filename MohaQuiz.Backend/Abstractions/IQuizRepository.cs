namespace MohaQuiz.Backend.Abstractions;

public interface IQuizRepository
{
    Task CreateTeam(string teamNameToCreate);
}
