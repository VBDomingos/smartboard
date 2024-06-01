using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Dashboard.Controllers;

public class DashboardController : Controller
{
    //get: /Dashboard/
    public string Index()
    {
        return "view1";
    }

    //get: /Dashboard/Something
    public string Something()
    {
        return "view2";
    }
}