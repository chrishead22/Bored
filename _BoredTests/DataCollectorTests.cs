using System;
using System.Net;
using Xunit;
using Moq;

namespace BoredTests;

public class DataCollectorTests
{
    [Fact]
    public static void GetApiResponse_OK()
    {
        string url = "https://www.boredapi.com/api/activity";
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();

        Assert.Equal("OK", response.StatusCode.ToString());
    }

    [Fact]
    public static void GetDatabaseResponse_OK()
    {
        Assert.True(new Context().Activities != null);
    }

    [Fact]
    public void GetActivities_ShouldDescription()
    {
        Mock<Models.Activity> mockActivity = new Mock<Models.Activity>();
        mockActivity.Object.Description = "descriptionTest";
        Assert.True(mockActivity.Object.Description == "descriptionTest");
    }
}