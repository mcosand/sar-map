using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Model.Gpx;
using Newtonsoft.Json;
using NUnit.Framework;
using Trackables.SarNodes;
using Trackables.SPOT;

namespace Tests.Trackable
{
  [TestFixture]
  public class SarNodeTrackerTests
  {
    [Test]
    public void GetSarNodeLocation()
    {
      SarNodeResponse responseSetup = new SarNodeResponse
      {
        Lat = 47.5029M,
        Lng = -121.9584M,
        Time = DateTime.Now
      };

      string asset = "myAsset";
      TestSarNodeTracker tracker = new TestSarNodeTracker(responseSetup);

      MapTrackable trackable = new MapTrackable
      {
        Name = "GetSpotLocation",
        ServiceId = "myFeed",
        ServiceType = "SPOT",
        MapTypeRef = JsonConvert.SerializeObject(new SarNodeParameters { NodeId = asset, Username = "user", Password = "secret" })
      };

      tracker.Poll(trackable);

      Console.WriteLine(trackable.MapObject.Data);

      var gpx = gpxType.FromString(trackable.MapObject.Data);
      var waypoint = gpx.wpt[0];
      Assert.AreEqual(responseSetup.Time, gpx.wpt[0].time, "waypoint time");
      Assert.AreEqual((double)responseSetup.Lat, gpx.wpt[0].lat, "waypoint lat");
      Assert.AreEqual((double)responseSetup.Lng, gpx.wpt[0].lon, "waypoint lng");
    }
  }

  public class TestSarNodeTracker : SarNodeTracker
  {
    public SarNodeResponse Response { get; set; }

    public TestSarNodeTracker(SarNodeResponse response) : base()
    {
      this.Response = response;
    }

    protected override SarNodeResponse Download(SarNodeParameters args)
    {
      return this.Response;
    }
  }
}
