using Microsoft.AspNetCore.Mvc.RazorPages;

using Models;

namespace Bored.Pages;

public class IndexModel : PageModel
{
    public Activity Activity { get; set; }

    public void OnGet()
    {
        Context context = new Context();
        Activity = DataCollector.DataCollector.GetActivityFromURL();
    }
}
