using System.Data;
using System.Diagnostics;

using BoredTests;
using Models;

namespace _DataCollector
{
    public class _DataCollector
    {
        public List<Models.Activity> GetActivitiesFromURL()
        {
            DataCollectorTests.GetApiResponse_OK();

            List<Models.Activity> activities = new List<Models.Activity>();

            return activities;
        }
    }
}