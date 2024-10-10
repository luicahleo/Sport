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
            var games = gameRepository.GetAllGames();
            foreach (var game in games)
            {
                if (game.HomeTeam == homeTeam && game.AwayTeam == awayTeam)
                {
                    gameRepository.RemoveGame(game);
                }
                break;
            }
        }

        public List<Game> GetSummary()
        {


            return gameRepository.GetAllGames();
        }

        public void UpdateScore(string homeTeam, string awayTeam, int newHomeScore, int newAwayScore)
        {
            var game = gameRepository.GetAllGames()
                         .FirstOrDefault(g => g.HomeTeam == homeTeam && g.AwayTeam == awayTeam);

            if (game != null)
            {
                int validHomeScore = newHomeScore < 0 ? 0 : newHomeScore;
                int validAwayScore = newAwayScore < 0 ? 0 : newAwayScore;

                game.UpdateScore(newHomeScore, newAwayScore);
            }
        }
    }
}
