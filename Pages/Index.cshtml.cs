using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitMQ.Client;

using Models;

namespace Bored.Pages;

public class IndexModel : PageModel
{
    public Activity Activity { get; set; }
    public Activity ActivityQueried { get; set; }

    public int TotalGood { get; set; }
    public int TotalBad { get; set; }
    public List<string> Types { get; set; }
    public List<Activity> Activities { get; set; }

    public void OnGet()
    {
        TotalGood = DataAnalyzer.DataAnalyzer.GetGoodActivities();
        TotalBad = DataAnalyzer.DataAnalyzer.GetBadActivities();
        Types = DataAnalyzer.DataAnalyzer.GetActivityTypes();

        Activity = DataCollector.DataCollector.GetRandomActivityFromURL();
        Activities = new Context().Activities.ToList();

        runMessageQueue(Activity);
    }

    private void runMessageQueue(Models.Activity activity)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "chris",
                Password = "password",
                VirtualHost = "/"
            };

            //else
            //{
            //factory = new ConnectionFactory
            //{
            //HostName = "https://bored-5028.azurewebsites.net/",
            //UserName = "chris",
            //Password = "password",
            //VirtualHost = "/"
            //};
            //}

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            MessageQueue.DirectExchangePublisher.Publish(channel, activity);
            MessageQueue.DirectExchangeConsumer.Consume(channel, activity);
        }
    }

    public IActionResult OnPostDoStuff(int id)
    {
        //reload first random activity
        Activity = DataCollector.DataCollector.GetByIDActivityFromURL(id);

        string description1 = Request.Form["description1"];
        string type1 = Request.Form["type1"];
        decimal price1 = Decimal.Parse(Request.Form["price1"]);
        decimal accessibility1 = Decimal.Parse(Request.Form["accessibility1"]);

        int? participants1 = null;
        if (!string.IsNullOrWhiteSpace(Request.Form["participants1"]))
            participants1 = Int32.Parse(Request.Form["participants1"]);

        string description2 = Request.Form["description2"];
        string type2 = Request.Form["type2"];

        decimal price2 = 0;
        if (!string.IsNullOrWhiteSpace(Request.Form["price2"]))
            price2 = Decimal.Parse(Request.Form["price2"]);

        decimal accessibility2 = 0;
        if (!string.IsNullOrWhiteSpace(Request.Form["accessibility2"]))
            accessibility2 = Decimal.Parse(Request.Form["accessibility2"]);

        int? participants2 = null;
        if (!string.IsNullOrWhiteSpace(Request.Form["participants2"]))
            participants2 = Int32.Parse(Request.Form["participants2"]);

        string longPrice2 = Request.Form["longPrice2"];
        string longAccessibility2 = Request.Form["longAccessibility2"];

        ActivityQueried = DataCollector.DataCollector.GetQueriedActivityFromURL(type2, participants2, longPrice2, longAccessibility2);

        if (!string.IsNullOrWhiteSpace(Request.Form["rbl1"]))
        {
            bool isGood1 = Request.Form["rbl1"] == "Good" ? true : false;
            DataCollector.DataCollector.SaveActivity(description1, type1, participants1, price1, accessibility1, isGood1);
        }
        else if (!string.IsNullOrWhiteSpace(Request.Form["rbl2"]))
        {
            bool isGood2 = Request.Form["rbl2"] == "Good" ? true : false;
            DataCollector.DataCollector.SaveActivity(description2, type2, participants2, price2, accessibility2, isGood2);
        }
        else
            DataCollector.DataCollector.SaveActivity(ActivityQueried.Description, ActivityQueried.Type, ActivityQueried.Participants, ActivityQueried.Price, ActivityQueried.Accessibility);

        TotalGood = DataAnalyzer.DataAnalyzer.GetGoodActivities();
        TotalBad = DataAnalyzer.DataAnalyzer.GetBadActivities();
        Types = DataAnalyzer.DataAnalyzer.GetActivityTypes();
        Activities = new Context().Activities.ToList();

        return Page();
    }
}
