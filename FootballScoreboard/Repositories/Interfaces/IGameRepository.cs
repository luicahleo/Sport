using FootballScoreboard.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballScoreboard.Repositories.Interfaces
{
    public interface IGameRepository
    {
        void AddGame(Game game);
        List<Game> GetAllGames();

    }
}
