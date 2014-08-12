namespace Trackables.SarNodes
{
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.Linq;
  using System.Net;
  using System.Text;
  using System.Threading.Tasks;
  using DataModel;
  using Newtonsoft.Json;

  public class SarNodeTracker : ITracker
  {
    public string Poll(MapTrackable trackable)
    {
      trackable.LastQuery = DateTime.Now;

      try
      {
        var parameters = JsonConvert.DeserializeObject<SarNodeParameters>(trackable.MapTypeRef);
        var response = Download(parameters);

        trackable.AppendUpdates(new List<MapTrackableUpdate> { new MapTrackableUpdate { Lat = response.Lat, Lng = response.Lng, Time = response.Time } });

        trackable.ServiceStatus = "OK";
        trackable.LastUpdate = DateTime.Now;
      }
      catch (Exception ex)
      {
        throw new TrackableUpdateException(ex.Message ?? "error", ex);
      }
      return "OK";
    }

    protected virtual SarNodeResponse Download(SarNodeParameters args)
    {
      WebClient client = new WebClient();
      client.Credentials = new NetworkCredential { UserName = args.Username, Password = args.Password };
      var template = ConfigurationManager.AppSettings["SarNodeApiTemplate"];
      if (template == null) throw new ApplicationException("Must set SarNodeApiTemplate in .config file");


      var response = client.DownloadString(string.Format(template, args.NodeId));
      return JsonConvert.DeserializeObject<SarNodeResponse>(response);
    }
  }
}
