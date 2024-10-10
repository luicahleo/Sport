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
        public DateTime StartTime { get; }

        public Game(string homeTeam, string awayTeam)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            HomeScore = 0;
            AwayScore = 0;
        }

        internal void UpdateScore(int newHomeScore, int newAwayScore)
        {
            HomeScore = newHomeScore < 0 ? 0 : newHomeScore;
            AwayScore = newAwayScore < 0 ? 0 : newAwayScore;

        }

        internal int TotalScore()
        {
            return HomeScore + AwayScore;
        }
    }
}
