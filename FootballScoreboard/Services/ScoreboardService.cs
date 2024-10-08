using FootballScoreboard.Models;
using FootballScoreboard.Repositories.Interfaces;
using FootballScoreboard.Services.Interfaces;

namespace FootballScoreboard.Services
{
    public class ScoreboardService : IScoreboardService
    {
        private readonly IGameRepository gameRepository;

        public ScoreboardService(IGameRepository repository)
        {
            gameRepository = repository;
        }

        public void StartGame(string homeTeam, string awayTeam)
        {
            var game = new Game(homeTeam, awayTeam);
            gameRepository.AddGame(game);
        }

        public void FinishGame(string homeTeam, string awayTeam)
        {
            
        }

        public List<Game> GetSummary()
        {


            return gameRepository.GetAllGames();
        }
    }
}
