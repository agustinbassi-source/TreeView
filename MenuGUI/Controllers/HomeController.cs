using MenuGUI.Controllers.Api;
using MenuGUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;
using Wigos.I18N.Modules.Common;

namespace MenuGUI.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      //var resourceMan = new ResourceManager("Wigos.I18N.Modules.GuiAuditory.Resources", typeof(Resources).Assembly);

      //var adasd = resourceMan.GetString("Legacy371", new CultureInfo("es"));

      //var asd = Wigos.I18N.Modules.GuiAuditory.Resources.Legacy1;

      //ResourceManager rm = new ResourceManager("rmc",
      //                typeof(MenuController).Assembly);

    //  rm.GetString("");

      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}