using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Gpx;

namespace DataModel
{
  public class MapTrackable
  {
    public Guid Id { get; set; }

    [ForeignKey("MapId")]
    public Map Map { get; set; }

    public Guid MapId { get; set; }

    [ForeignKey("MapObjectId")]
    public MapObject MapObject { get; set; }

    public Guid? MapObjectId { get; set; }

    public string ServiceType { get; set; }

    public string ServiceId { get; set; }

    public string ServiceStatus { get; set; }

    public DateTime? LastUpdate { get; set; }

    public DateTime? LastQuery { get; set; }

    public string MapType { get; set; }

    public string MapTypeRef { get; set; }

    public string Name { get; set; }

    public MapTrackable()
    {
      this.Id = Guid.NewGuid();
    }

    public void AppendUpdates(List<MapTrackableUpdate> updates)
    {
      if (this.MapObjectId == null && this.MapObject == null)
      {
        this.MapObject = new MapObject
        {
          Name = this.Name
        };
      }

      string currentWptName = this.Name + ": Current";

      //        <trkpt lat=""47.4456391"" lon=""-121.4242937"">
      //<ele>951.481</ele>
      //<time>2014-04-27T21:30:06.347Z</time>
      //</trkpt>
      gpxType gpx;
      if (string.IsNullOrWhiteSpace(this.MapObject.Data))
      {
        gpx = new gpxType();
        gpx.trk.Add(new trkType { name = "History" });
        gpx.trk[0].trkseg.Add(new trksegType());
      }
      else
      {
        gpx = gpxType.FromString(this.MapObject.Data);
      }

      var segment = gpx.trk[0].trkseg[0];
      var lastPoint = segment.trkpt.LastOrDefault();

      int cursor = updates.Count - 1;
      while (lastPoint != null && updates[cursor-- - 1].Time <= lastPoint.time) ;

      while (cursor >= 0)
      {
        var msg = updates[cursor];
        var pt = new wptType
        {
          lat = msg.Lat,
          lon = msg.Lng,
          time = msg.Time,
          timeSpecified = true
        };
        segment.trkpt.Add(pt);
        if (!string.IsNullOrWhiteSpace(msg.Message))
        {
          gpx.wpt.Add(new wptType
          {
            timeSpecified = true,
            time = msg.Time,
            lat = msg.Lat,
            lon = msg.Lng,
            cmt = msg.Message,
            name = string.Format("{0}: {1} @{2:HH:mm}", this.Name, msg.MessageType, msg.Time)
          });
        }

        cursor--;
      }

      var wptCurrent = gpx.wpt.Where(f => f.name == currentWptName).SingleOrDefault();
      if (wptCurrent == null)
      {
        wptCurrent = new wptType
        {
          name = currentWptName
        };
        gpx.wpt.Add(wptCurrent);
      }
      wptCurrent.time = updates[0].Time;
      wptCurrent.timeSpecified = true;
      wptCurrent.lat = updates[0].Lat;
      wptCurrent.lon = updates[0].Lng;

      this.MapObject.Update(gpx);
    }
  }

  public class TrackableUpdateException : ApplicationException
  {
    public TrackableUpdateException(string message, Exception inner)
      : base(message, inner)
    {
    }
  }
}
