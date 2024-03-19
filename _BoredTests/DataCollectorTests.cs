using System;
using System.Net;
using Xunit;

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
}