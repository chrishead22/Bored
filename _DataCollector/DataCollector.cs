using System.Data;
using System.Diagnostics;

using BoredTests;

namespace _DataCollector
{
    public class _DataCollector
    {
        public List<_Models.Activity> GetActivitiesFromURL()
        {
            DataCollectorTests.GetApiResponse_OK();

            List<_Models.Activity> activities = new List<_Models.Activity>();

            return activities;
        }
    }
}