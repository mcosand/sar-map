namespace angular_leaflet.Api
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity.Spatial;
  using System.Linq;
  using System.Linq.Expressions;
  using System.Net;
  using System.Net.Http;
  using System.Web.Http;
  using angular_leaflet.Api.Models;
  using GeoJSON.Net;
  using GeoJSON.Net.Feature;
  using GeoJSON.Net.Geometry;
  using Data = DataModel;

  public class MapObjectController : BaseMapApiController
  {
    private IGeometryObject GeoJsonFromDbGeography(DbGeography shape)
    {
      IGeometryObject geometry = null;
      switch (shape.SpatialTypeName)
      {
        case "Point":
          geometry = new Point(new GeographicPosition(shape.Latitude.Value, shape.Longitude.Value));
          break;
        case "LineString":
          List<IPosition> points = new List<IPosition>();
          for (int i=0; i<shape.PointCount; i++)
          {
            DbGeography point = shape.PointAt(i + 1);
            points.Add(new GeographicPosition(point.Latitude.Value, point.Longitude.Value));
          }
          geometry = new LineString(points);
          break;
        default:
          break;
      }
      return geometry;
    }

    private Feature FeatureFromMapObject(Data.MapObject item)
    {
      Feature feature = new Feature(
        GeoJsonFromDbGeography(item.Shape),
        new Dictionary<string, object> {
          { "Name", item.Name },
          { "Text", item.Text }
        },
        item.Id.ToString());
      if (!string.IsNullOrWhiteSpace(item.Class))
      {
        feature.Properties.Add("Class", item.Class);
      }
      return feature;
    }

    private List<FeatureCollection> FeatureCollectionFromMapObjects(IEnumerable<Data.MapObject> objects)
    {
      var objectsList = new List<FeatureCollection>();

      foreach (var topLevel in objects.Where(f => f.ParentId == null))
      {
        List<Feature> features = new List<Feature>();
        if (topLevel.Children.Count == 0)
        {
          features.Add(FeatureFromMapObject(topLevel));
        }
        else
        {
          features.AddRange(topLevel.Children.Select(f => FeatureFromMapObject(f)));
        }
        objectsList.Add(new FeatureCollection(features));
      }
      return objectsList;
    }

    // GET api/map
    public IEnumerable<GeoJSONObject> Get(Guid mapId)
    {

      using (var db = GetDb())
      {
        var objects = db.Maps.Where(f => f.Id == mapId).SelectMany(f => f.Objects);

        var foo = objects.ToList();

        var list = FeatureCollectionFromMapObjects(objects);
        return list;
      }
    }

    // GET api/map/5
    public FeatureCollection Get(Guid mapId, Guid id)
    {
      using (var db = GetDb())
      {
        var objects = db.Maps.Where(f => f.Id == mapId).SelectMany(f => f.Objects).Where(f => f.Id == id || f.ParentId == id);
        return FeatureCollectionFromMapObjects(objects).FirstOrDefault();
      }
    }

    //// POST api/map
    //public Map Post([FromBody]Map value)
    //{
    //  using (var db = GetDb())
    //  {
    //    var map = new Data.Map
    //    {
    //      Name = value.Name,
    //      Lat = value.Lat,
    //      Lng = value.Lng,
    //      Zoom = value.Zoom,
    //      Created = DateTime.Now
    //    };
    //    db.Maps.Add(map);
    //    db.SaveChanges();
    //    return mapConv(map);
    //  }
    //}

    //// PUT api/map/5
    //public void Put(int id, [FromBody]string value)
    //{
    //}

    //// DELETE api/map/5
    //public void Delete(int id)
    //{
    //}
  }
}
