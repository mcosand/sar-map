using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Types;
using Model.Gpx;

namespace DataModel
{
  public class MapObject
  {
    internal const decimal coordEpsilon = 0.00001M;

    public MapObject()
    {
      this.Id = Guid.NewGuid();
      this.Children = new List<MapObject>();
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Class { get; set; }
    public string Text { get; set; }
    public DbGeography Shape { get; set; }
    
    public string Data { get; set; }

    [ForeignKey("ParentId")]
    public MapObject Parent { get; set; }
    public Guid? ParentId { get; set; }

    public ICollection<MapObject> Children { get; set; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="gpx"></param>
    public void Update(gpxType gpx)
    {
      if (this.ParentId != null || this.Parent != null)
      {
        throw new InvalidOperationException("Can't update the child. Update the parent");
      }

      this.Data = gpx.ToString();
      while (this.Children.Count > 0)
      {
        this.Children.Remove(this.Children.First());
      }

      foreach (var trk in gpx.trk)
      {
        SqlGeometry geom = SqlGeometry.STGeomFromText(new SqlChars(BuildLineString(trk)), DbGeography.DefaultCoordinateSystemId);
        if (!geom.STIsValid())
        {
          geom.MakeValid();
        }

        MapObject child = new MapObject
        {
          Name = trk.name ?? "unknown",
          Shape = DbGeography.FromBinary(geom.STAsBinary().Value),
          Parent = this
        };
        this.Children.Add(child);
      }

      foreach (var wpt in gpx.wpt)
      {
        MapObject child = new MapObject
        {
          Name = wpt.name ?? "unknown",
          Shape = DbGeography.PointFromText(string.Format("POINT({0} {1})", wpt.lon, wpt.lat), DbGeography.DefaultCoordinateSystemId),
          Text = string.Format("{0}{1}", wpt.desc, (wpt.cmt == null || wpt.cmt == wpt.desc) ? string.Empty : ("\n" + wpt.cmt)),
          Parent = this
        };
        this.Children.Add(child);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="track"></param>
    /// <returns></returns>
    internal static string BuildLineString(trkType track)
    {
      StringBuilder sb = new StringBuilder(
        (track.trkseg.Count == 1) ? "LINESTRING" : "MULTILINESTRING(");

      if (track.trkseg.SelectMany(f => f.trkpt).Count() == 0)
      {
        return null;
      }

      for (int i = 0; i < track.trkseg.Count; i++)
      {
        if (track.trkseg[i].trkpt.Count == 0) continue;

        sb.Append((i == 0) ? "(" : ",(");
        for (int j = 0; j < track.trkseg[i].trkpt.Count; j++)
        {
          var pt = track.trkseg[i].trkpt[j];
          sb.AppendFormat("{0}{1} {2}", (j == 0) ? string.Empty : ",",
            pt.lon, pt.lat);
        }
        if (track.trkseg[i].trkpt.Count == 1)
        {
          var pt = track.trkseg[i].trkpt[0];
          sb.AppendFormat(",{0} {1}", pt.lon, pt.lat + coordEpsilon);
        }
        sb.Append(")");
      }

      if (track.trkseg.Count > 1)
      {
        sb.Append(")");
      }

      return sb.ToString();
    }
  }
}
