using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Models;

namespace Bored.Pages;

public class IndexModel : PageModel
{
    public Activity Activity { get; set; }
    public Activity ActivityQueried { get; set; }

    public int TotalGood { get; set; }
    public int TotalBad { get; set; }

    public void OnGet()
    {
        TotalGood = DataAnalyzer.DataAnalyzer.GetGoodActivities();
        TotalBad = DataAnalyzer.DataAnalyzer.GetBadActivities();

        Activity = DataCollector.DataCollector.GetRandomActivityFromURL();
    }

    public IActionResult OnPostGetData(int id)
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

        ActivityQueried = DataCollector.DataCollector.GetQueriedActivityFromURL(type2, participants2, longPrice2, longAccessibility2);

        TotalGood = DataAnalyzer.DataAnalyzer.GetGoodActivities();
        TotalBad = DataAnalyzer.DataAnalyzer.GetBadActivities();

        return Page();
    }
}
