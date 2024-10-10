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

        fixture.Customize(new FootballTeamCustomization());
    }

    [Fact]
    public void CanStartGame_WithInitialScoreOfZero()
    {
        string homeTeam = fixture.Create<string>();
        string awayTeam = fixture.Create<string>();

        scoreboard.StartGame(homeTeam, awayTeam);

        while (homeTeam == awayTeam)
        {
            awayTeam = fixture.Create<string>();
        }

        // Assert: El marcador debería tener un juego con puntuación inicial de 0-0
        var summary = scoreboard.GetSummary();
        Assert.Single(summary);
        Assert.Equal(homeTeam, summary[0].HomeTeam);
        Assert.Equal(awayTeam, summary[0].AwayTeam);
        Assert.Equal(0, summary[0].HomeScore);
        Assert.Equal(0, summary[0].AwayScore);
    }

    [Fact]
    public void FinishGame_RemovesGameFromScoreboard()
    {
        string homeTeam = fixture.Create<string>();
        string awayTeam = fixture.Create<string>();

        while (homeTeam == awayTeam)
        {
            awayTeam = fixture.Create<string>();
        }
        //Iniciamos y luego lo finalizamos
        scoreboard.StartGame(homeTeam, awayTeam);
        scoreboard.FinishGame(homeTeam, awayTeam);

        // Assert: El marcador debería estar vacío
        var summary = scoreboard.GetSummary();
        Assert.Empty(summary);
    }

    [Fact]
    public void FinishGame_GameDoesNotExist_NoChangesToScoreboard()
    {
        string homeTeam = fixture.Create<string>();
        string awayTeam = fixture.Create<string>();

        while (homeTeam == awayTeam)
        {
            awayTeam = fixture.Create<string>();
        }
        scoreboard.StartGame(homeTeam, awayTeam);

        string nonExistentHomeTeam = fixture.Create<string>();
        string nonExistentAwayTeam = fixture.Create<string>();

        while (nonExistentHomeTeam == nonExistentAwayTeam)
        {
            nonExistentAwayTeam = fixture.Create<string>();
        }
        scoreboard.FinishGame(nonExistentHomeTeam, nonExistentAwayTeam);

        // Assert: Verificar que el juego original sigue existiendo
        var summary = scoreboard.GetSummary();
        Assert.Single(summary); 
        Assert.Equal(homeTeam, summary[0].HomeTeam);
        Assert.Equal(awayTeam, summary[0].AwayTeam);
    }
}