using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoFixture;

using FootballScoreboard.Repositories;
using FootballScoreboard.Repositories.Interfaces;
using FootballScoreboard.Services;
using FootballScoreboard.Services.Interfaces;

namespace FootballScoreboard.Tests;

public class ScoreboardAutofixtureTest
{
    private readonly Fixture fixture;
    private IScoreboardService scoreboard;
    private IGameRepository repository;

    public ScoreboardAutofixtureTest()
    {
        fixture = new Fixture();
        repository = new InMemoryGameRepository();
        scoreboard = new ScoreboardService(repository);

    }

    [Fact]
    public void CanStartGame_WithInitialScoreOfZero()
    {
        string homeTeam = fixture.Create<string>();
        string awayTeam = fixture.Create<string>();

        scoreboard.StartGame(homeTeam, awayTeam);

        // Assert: El marcador debería tener un juego con puntuación inicial de 0-0
        var summary = scoreboard.GetSummary();
        Assert.Single(summary);
        Assert.Equal(homeTeam, summary[0].HomeTeam);
        Assert.Equal(awayTeam, summary[0].AwayTeam);
        Assert.Equal(0, summary[0].HomeScore);
        Assert.Equal(0, summary[0].AwayScore);
    }
}
