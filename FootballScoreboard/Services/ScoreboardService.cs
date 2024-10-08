using FootballScoreboard.Models;
using FootballScoreboard.Services.Interfaces;

namespace FootballScoreboard.Services
{
    public class ScoreboardService : IScoreboardService
    {
        private readonly List<Game> games = new List<Game>();

        public void StartGame(string homeTeam, string awayTeam)
        {
            var game = new Game(homeTeam, awayTeam);
            games.Add(game);
        }

        public void FinishGame(string homeTeam, string awayTeam)
        {
            
        }
    }
}
