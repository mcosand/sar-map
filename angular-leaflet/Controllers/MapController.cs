using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;

namespace angular_leaflet.Controllers
{
  public class MapController : Controller
  {
    public ActionResult Index()
    {
      return View();
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
