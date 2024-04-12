using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Models;

namespace Bored.Pages;

public class IndexModel : PageModel
{
    public Activity Activity { get; set; }
    public Activity ActivityQueried { get; set; }

    public int Attempted { get; set; }
    public int Completed { get; set; }

    public void OnGet()
    {
        Context context = new Context();
        Attempted = context.Activities.Sum(x => x.Attempted);
        Completed = context.Activities.Sum(x => x.Completed);

        Activity = DataCollector.DataCollector.GetRandomActivityFromURL();
    }

    public IActionResult OnPostGetData(int id)
    {
        //reload first random activity
        Activity = DataCollector.DataCollector.GetByIDActivityFromURL(id);

        string type = Request.Form["type"];
        string price = Request.Form["price"];
        string accessibility = Request.Form["accessibility"];

        int? participants = null;
        if (!string.IsNullOrWhiteSpace(Request.Form["participants"]))
            participants = Int32.Parse(Request.Form["participants"]);

        ActivityQueried = DataCollector.DataCollector.GetQueriedActivityFromURL(type, participants, price, accessibility);

        return Page();
    }
}
