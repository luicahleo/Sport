using FootballScoreboard.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballScoreboard.Services.Interfaces
{
    public interface IScoreboardService
    {
        void StartGame(string homeTeam, string awayTeam);
        void FinishGame(string homeTeam, string awayTeam);
        List<Game> GetSummary();

    }
}
