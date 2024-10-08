using FootballScoreboard.Models;
using FootballScoreboard.Repositories.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballScoreboard.Repositories
{
    public class InMemoryGameRepository : IGameRepository
    {
        private readonly List<Game> games = new List<Game>();

        public void AddGame(Game game)
        {
            games.Add(game);
        }
    }
}
