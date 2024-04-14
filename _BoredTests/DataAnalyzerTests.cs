using System;
using System.Net;
using Xunit;

namespace BoredTests;

public class DataAnalyzerTests
{
    [Fact]
    public static void GetGoodActivitiesPositive_OK()
    {
        Assert.True(DataAnalyzer.DataAnalyzer.GetGoodActivities() > -1);
    }

    [Fact]
    public static void GetBadActivitiesPositive_OK()
    {
        Assert.True(DataAnalyzer.DataAnalyzer.GetBadActivities() > -1);
    }
}