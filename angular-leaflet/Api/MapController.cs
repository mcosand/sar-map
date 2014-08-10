namespace angular_leaflet.Api
{
  using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using angular_leaflet.Api.Models;
using Data = DataModel;

  public class MapController : ApiController
  {
    private Data.MapContext GetDb()
    {
      return new Data.MapContext("MapContext");
    }

    private static Expression<Func<Data.Map, Map>> mapExpr = f => new Map
        {
          Id = f.Id,
          Name = f.Name ?? "unknown",
          Lat = f.Lat,
          Lng = f.Lng,
          Zoom = f.Zoom,
          Created = f.Created
        };
    private static Func<Data.Map, Map> mapConv = mapExpr.Compile();

    // GET api/map
    public IEnumerable<Map> Get()
    {
      using (var db = GetDb())
      {
        return db.Maps.Select(mapExpr)
        .OrderByDescending(f => f.Created).ToList();
      }
    }

    // GET api/map/5
    public Map Get(Guid id)
    {
      using (var db = GetDb())
      {
        return db.Maps.Where(f => f.Id == id).Select(mapExpr).SingleOrDefault();
      }
    }

    // POST api/map
    public Map Post([FromBody]Map value)
    {
      using (var db = GetDb())
      {
        var map = new Data.Map
        {
          Name = value.Name,
          Lat = value.Lat,
          Lng = value.Lng,
          Zoom = value.Zoom,
          Created = DateTime.Now
        };
        db.Maps.Add(map);
        db.SaveChanges();
        return mapConv(map);
      }
    }

    // PUT api/map/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/map/5
    public void Delete(int id)
    {
    }
  }
}
