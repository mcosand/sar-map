using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;

namespace angular_leaflet.Controllers
{
  public class AdminController : Controller
  {
    public ActionResult UpdateDatabase()
    {
      var mapStoreString = ConfigurationManager.ConnectionStrings["MapContext"].ConnectionString;
      DataModel.Migrations.Migrator.UpdateDatabase(mapStoreString);

      return Content("Finished");
    }

    [HttpGet]
    public ActionResult Create()
    {
      return View();
    }

    public ActionResult View(Guid id)
    {
      var api = new Api.MapController();
      var map = api.Get(id);
      
      return View(map);
    }
  }
}
