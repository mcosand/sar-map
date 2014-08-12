using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Model.Gpx;
using NUnit.Framework;
using Trackables.SPOT;

namespace Tests.DataModel
{
  [TestFixture]
  public class MapObjectTests
  {
    [Test]
    public void TrackToString()
    {
      trkType trk = GetBasicTrack();

      Assert.AreEqual(
        "LINESTRING(-122.01 47.5,-122.10 47.6,-122.05 47.7)",
        MapObject.BuildLineString(trk));
    }

    [Test]
    public void TrackToString_Multiple()
    {
      var track = GetBasicTrack();
      var seg = new trksegType();
      seg.trkpt.Add(new wptType { lat = 47.6M, lon = -122.07M });
      seg.trkpt.Add(new wptType { lat = 47.63M, lon = -122.08M });
      track.trkseg.Add(seg);

      Assert.AreEqual(
        "MULTILINESTRING((-122.01 47.5,-122.10 47.6,-122.05 47.7),(-122.07 47.6,-122.08 47.63))",
        MapObject.BuildLineString(track));
    }

    [Test]
    public void TrackToString_OnePoint()
    {
      var track = GetBasicTrack();
      track.trkseg[0].trkpt.RemoveRange(1, track.trkseg[0].trkpt.Count - 1);

      Assert.AreEqual(
        "LINESTRING(-122.01 47.5,-122.01 47.50001)",
        MapObject.BuildLineString(track));
    }

    private static trkType GetBasicTrack()
    {
      trkType trk = new trkType();
      trksegType seg = new trksegType();
      trk.trkseg.Add(seg);
      seg.trkpt.Add(new wptType { lat = 47.5M, lon = -122.01M, time = DateTime.Now, timeSpecified = true });
      seg.trkpt.Add(new wptType { lat = 47.6M, lon = -122.10M, time = DateTime.Now, timeSpecified = true });
      seg.trkpt.Add(new wptType { lat = 47.7M, lon = -122.05M, time = DateTime.Now, timeSpecified = true });
      return trk;
    }
  }
}
