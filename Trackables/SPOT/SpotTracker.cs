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

        trackable.AppendUpdates(updates.Select(f => new MapTrackableUpdate {
          Lat = f.latitude,
          Lng = f.longitude,
          Time = f.dateTime,
          Message = f.messageContent,
          MessageType = f.messageType
        }).ToList());

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
