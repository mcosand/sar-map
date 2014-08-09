using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Model.Gpx;
using Newtonsoft.Json;

namespace Trackables.SPOT
{
  public class SpotTracker : ITracker
  {
    public string Poll(MapTrackable trackable)
    {
      trackable.LastQuery = DateTime.Now;
      try
      {
        string feedJson = DownloadFeed();
        var feed = JsonConvert.DeserializeObject<SpotResponseContainer>(feedJson);
        var updates = feed.response.feedMessageResponse.messages.message;

        if (trackable.MapObjectId == null && trackable.MapObject == null)
        {
          trackable.MapObject = new MapObject
          {
            Name = trackable.Name
          };
        }

        string currentWptName = trackable.Name + ": Current";

//        <trkpt lat=""47.4456391"" lon=""-121.4242937"">
//<ele>951.481</ele>
//<time>2014-04-27T21:30:06.347Z</time>
//</trkpt>
        gpxType gpx;
        if (string.IsNullOrWhiteSpace(trackable.MapObject.Data))
        {
          gpx = new gpxType();
          gpx.trk.Add(new trkType { name = "History" });
          gpx.trk[0].trkseg.Add(new trksegType());
        }
        else
        {
          gpx = gpxType.FromString(trackable.MapObject.Data);
        }

        var segment = gpx.trk[0].trkseg[0];
        var lastPoint = segment.trkpt.LastOrDefault();

        int cursor = updates.Length - 1;
        while (lastPoint != null && updates[cursor-- - 1].dateTime <= lastPoint.time) ;

        while (cursor >= 0)
        {
          var msg = updates[cursor];
          var pt = new wptType {
            lat = msg.latitude,
            lon = msg.longitude,
            time = msg.dateTime,
            timeSpecified = true
          };
          segment.trkpt.Add(pt);
          if (!string.IsNullOrWhiteSpace(msg.messageContent))
          {
            gpx.wpt.Add(new wptType
            {
              timeSpecified = true,
              time = msg.dateTime,
              lat = msg.latitude,
              lon = msg.longitude,
              cmt = msg.messageContent,
              name = string.Format("{0}: {1} @{2:HH:mm}", trackable.Name, msg.messageType, msg.dateTime)
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
        wptCurrent.time = updates[0].dateTime;
        wptCurrent.lat = updates[0].latitude;
        wptCurrent.lon = updates[0].longitude;

        trackable.MapObject.Update(gpx);

        // calc new shape

        trackable.ServiceStatus = "OK";
        trackable.LastUpdate = DateTime.Now;
      }
      catch (Exception ex)
      {
        throw new TrackableUpdateException(ex.Message ?? "error", ex);
      }

      return "OK";
    }

    protected virtual string DownloadFeed()
    {
      return "{}";
    }
  }
}
