using System.Data;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;

using BoredTests;
using Models;

namespace DataCollector
{
    public class DataCollector
    {
        public static Models.Activity GetActivityFromURL()
        {
            DataCollectorTests.GetApiResponse_OK();

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("https://www.boredapi.com/api/activity");
            string cleanedJSON = json.Replace("key\"", "ID\"").Replace("activity\"", "Description\"");

            return JsonConvert.DeserializeObject<Models.Activity>(cleanedJSON);
        }
    }
}