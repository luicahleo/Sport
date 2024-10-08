using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballScoreboard.Models
{
    public class Game
    {
        public string HomeTeam { get; }
        public string AwayTeam { get; }
        public int HomeScore { get; private set; }
        public int AwayScore { get; private set; }

        public Game(string homeTeam, string awayTeam)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            HomeScore = 0;
            AwayScore = 0;
        }
    }
}
