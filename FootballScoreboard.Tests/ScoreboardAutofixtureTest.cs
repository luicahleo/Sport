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

    #region UpdateScore

    [Fact]
    public void UpdateScore_UpdatesScoreCorrectly()
    {
        string homeTeam = fixture.Create<string>();
        string awayTeam = fixture.Create<string>();

        while (homeTeam == awayTeam)
        {
            awayTeam = fixture.Create<string>();
        }

        scoreboard.StartGame(homeTeam, awayTeam);

        int newHomeScore = 1;
        int newAwayScore = 2;
        scoreboard.UpdateScore(homeTeam, awayTeam, newHomeScore, newAwayScore);

        // Assert: Verificamos que el marcador se haya actualizado correctamente
        var summary = scoreboard.GetSummary();
        Assert.Single(summary);
        Assert.Equal(newHomeScore, summary[0].HomeScore);
        Assert.Equal(newAwayScore, summary[0].AwayScore);
    }

    [Fact]
    public void UpdateScore_DoesNotAllowNegativeScores()
    {
        string homeTeam = fixture.Create<string>();
        string awayTeam = fixture.Create<string>();
        while (homeTeam == awayTeam)
        {
            awayTeam = fixture.Create<string>();
        }

        scoreboard.StartGame(homeTeam, awayTeam);
        scoreboard.UpdateScore(homeTeam, awayTeam, -1, 3);

        // Assert: Verificar que los puntajes negativos no sean permitidos
        var summary = scoreboard.GetSummary();
        Assert.Single(summary);

    }

    #endregion

    #region Example of System
    [Fact]
    public void GetSummary_ReturnsGamesOrderedByTotalScoreAndMostRecent()
    {

        scoreboard.StartGame("Mexico", "Canada");
        scoreboard.UpdateScore("Mexico", "Canada", 0, 5);

        scoreboard.StartGame("Spain", "Brazil");
        scoreboard.UpdateScore("Spain", "Brazil", 10, 2);

        scoreboard.StartGame("Germany", "France");
        scoreboard.UpdateScore("Germany", "France", 2, 2);

        scoreboard.StartGame("Uruguay", "Italy");
        scoreboard.UpdateScore("Uruguay", "Italy", 6, 6);

        scoreboard.StartGame("Argentina", "Australia");
        scoreboard.UpdateScore("Argentina", "Australia", 3, 1);

        var summary = scoreboard.GetSummary();

        // Assert: El orden tiene que estar como en Example, siguiendo el orden de suma de marcadores asc
        Assert.Equal("Uruguay", summary[0].HomeTeam);
        Assert.Equal("Italy", summary[0].AwayTeam);
        Assert.Equal(6, summary[0].HomeScore);
        Assert.Equal(6, summary[0].AwayScore);

        Assert.Equal("Spain", summary[1].HomeTeam);
        Assert.Equal("Brazil", summary[1].AwayTeam);
        Assert.Equal(10, summary[1].HomeScore);
        Assert.Equal(2, summary[1].AwayScore);

        Assert.Equal("Mexico", summary[2].HomeTeam);
        Assert.Equal("Canada", summary[2].AwayTeam);
        Assert.Equal(0, summary[2].HomeScore);
        Assert.Equal(5, summary[2].AwayScore);

        Assert.Equal("Argentina", summary[3].HomeTeam);
        Assert.Equal("Australia", summary[3].AwayTeam);
        Assert.Equal(3, summary[3].HomeScore);
        Assert.Equal(1, summary[3].AwayScore);

        Assert.Equal("Germany", summary[4].HomeTeam);
        Assert.Equal("France", summary[4].AwayTeam);
        Assert.Equal(2, summary[4].HomeScore);
        Assert.Equal(2, summary[4].AwayScore);

    }



    #endregion


}