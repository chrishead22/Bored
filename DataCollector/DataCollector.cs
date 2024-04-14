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
        public static Models.Activity GetRandomActivityFromURL()
        {
            DataCollectorTests.GetApiResponse_OK();

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("https://www.boredapi.com/api/activity");
            string cleanedJSON = json.Replace("key\"", "ID\"").Replace("activity\"", "Description\"");

            return JsonConvert.DeserializeObject<Models.Activity>(cleanedJSON);
        }

        public static Models.Activity GetByIDActivityFromURL(int id)
        {
            DataCollectorTests.GetApiResponse_OK();

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("https://www.boredapi.com/api/activity?key=" + id);
            string cleanedJSON = json.Replace("key\"", "ID\"").Replace("activity\"", "Description\"");

            return JsonConvert.DeserializeObject<Models.Activity>(cleanedJSON);
        }

        public static Models.Activity GetQueriedActivityFromURL(string type, int? participants, string price, string accessibility)
        {
            DataCollectorTests.GetApiResponse_OK();

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            string url = "https://www.boredapi.com/api/activity?";

            if (!string.IsNullOrWhiteSpace(type))
                url += "&type=" + type;
            if (participants.HasValue)
                url += "&participants=" + participants;
            if (!string.IsNullOrWhiteSpace(price))
            {
                if (price == "Free")
                    url += "&minprice=" + 0 + "&maxprice=" + 0;
                else if (price == "Most likely cheap")
                    url += "&minprice=" + 0.01 + "&maxprice=" + 0.2;
                else if (price == "Approximately moderate")
                    url += "&minprice=" + 0.21 + "&maxprice=" + 0.66;
                else if (price == "Potentially expensive")
                    url += "&minprice=" + 0.67 + "&maxprice=" + 1.0;
            }
            if (!string.IsNullOrWhiteSpace(accessibility))
            {
                if (accessibility == "Few to no challenges")
                    url += "&minaccessibility=" + 0 + "&maxaccessibility=" + 0.3;
                else if (accessibility == "Minor challenges")
                    url += "&minaccessibility=" + 0.31 + "&maxaccessibility=" + 0.66;
                else if (accessibility == "Major challenges")
                    url += "&minaccessibility=" + 0.67 + "&maxaccessibility=" + 1.0;
            }

            string json = (new WebClient()).DownloadString(url);
            string cleanedJSON = json.Replace("key\"", "ID\"").Replace("activity\"", "Description\"").Replace("?&", "?");

            return JsonConvert.DeserializeObject<Models.Activity>(cleanedJSON);
        }

        public static void SaveActivity(string description, string type, int? participants, decimal price, decimal accessibility, bool isGood)
        {
            Context context = new Context();
            Models.Activity activity = GetActivityByDescriptionAndType(description, type);

            if (activity == null)
            {
                activity = new Models.Activity();
                context.Activities.Add(activity);
                activity.Description = description;
                activity.Type = type;
                activity.Participants = participants.Value;
                activity.Price = price;
                activity.Accessibility = accessibility;
            }

            activity.Good = isGood;
            activity.Bad = !isGood;
            context.SaveChanges();
        }

        public static void SaveActivity(string description, string type, int? participants, decimal price, decimal accessibility)
        {
            Models.Activity activity = GetActivityByDescriptionAndType(description, type);

            if (activity == null)
            {
                Context context = new Context();
                activity = new Models.Activity();
                context.Activities.Add(activity);
                activity.Description = description;
                activity.Type = type;
                activity.Participants = participants.Value;
                activity.Price = price;
                activity.Accessibility = accessibility;
                activity.Good = false;
                activity.Bad = false;
                context.SaveChanges();
            }
        }

        public static Models.Activity GetActivityByDescriptionAndType(string description, string type)
        {
            return new Context().Activities.FirstOrDefault(x => x.Description == description && x.Type == type);
        }
    }
}