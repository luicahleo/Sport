using AutoFixture;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballScoreboard.Tests;

public class FootballTeamCustomization : ICustomization
{
    private static readonly List<string> Teams = new List<string>
{
    "Mexico", "Canada", "Spain", "Brazil", "Germany", "France",
    "Uruguay", "Italy", "Argentina", "Australia"
};

    public void Customize(IFixture fixture)
    {
        fixture.Customize<string>(c => c.FromFactory(() => Teams[fixture.Create<int>() % Teams.Count]));
    }
}
