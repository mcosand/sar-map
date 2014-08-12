using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Model.Gpx;
using NUnit.Framework;
using Trackables.SPOT;

namespace Tests.Trackable
{
  [TestFixture]
  public class SpotTrackerTests
  {
    [Test]
    public void GetSpotLocation()
    {
      TestSpotTracker tracker = new TestSpotTracker();
      MapTrackable trackable = new MapTrackable
      {
        Name = "GetSpotLocation",
        ServiceId = "myFeed",
        ServiceType = "SPOT"
      };

      tracker.NextResponse = responses[0];
      tracker.Poll(trackable);

      Console.WriteLine(trackable.MapObject.Data);
    }

    [Test]
    public void GetSpotLocationUpdates()
    {
      TestSpotTracker tracker = new TestSpotTracker();
      MapTrackable trackable = new MapTrackable
      {
        Name = "Trackable Test",
        ServiceId = "myFeed",
        ServiceType = "SPOT"
      };

      tracker.NextResponse = responses[0];
      tracker.Poll(trackable);
      Assert.IsNotNull(trackable.MapObject.Data, "map data not null");
      tracker.NextResponse = responses[1];
      tracker.Poll(trackable);
      Assert.IsNotNull(trackable.MapObject.Data, "map data not null");

      Assert.AreEqual(1, trackable.MapObject.Children.Where(f => f.Shape.Dimension == 1).Count(), "child count 2");
      Assert.AreEqual(36, trackable.MapObject.Children.First().Shape.PointCount, "points 2");
      var gpx = gpxType.FromString(trackable.MapObject.Data);
      Assert.AreEqual(DateTime.Parse("2014-07-26T18:56:41+0000"), gpx.trk[0].trkseg[0].trkpt[0].time, "first point time 2");
      Assert.AreEqual(DateTime.Parse("2014-07-27T02:36:00+0000"), gpx.trk[0].trkseg[0].trkpt.Last().time, "last point time 2");

      tracker.NextResponse = responses[2];
      tracker.Poll(trackable);
      Assert.AreEqual(1, trackable.MapObject.Children.Where(f => f.Shape.Dimension == 1).Count(), "child count 3");
      Assert.AreEqual(52, trackable.MapObject.Children.First().Shape.PointCount, "points 3");
      gpx = gpxType.FromString(trackable.MapObject.Data);
      Assert.AreEqual(DateTime.Parse("2014-07-26T18:56:41+0000"), gpx.trk[0].trkseg[0].trkpt[0].time, "first point time 3");
      Assert.AreEqual(DateTime.Parse("2014-07-27T23:00:41+0000"), gpx.trk[0].trkseg[0].trkpt.Last().time, "last point time 3");

      tracker.NextResponse = responses[3];
      tracker.Poll(trackable);
      Assert.AreEqual(1, trackable.MapObject.Children.Where(f => f.Shape.Dimension == 1).Count(), "child count 4");
      Assert.AreEqual(63, trackable.MapObject.Children.First().Shape.PointCount, "points 4");
      gpx = gpxType.FromString(trackable.MapObject.Data);
      Assert.AreEqual(DateTime.Parse("2014-07-26T18:56:41+0000"), gpx.trk[0].trkseg[0].trkpt[0].time, "first point time 4");
      Assert.AreEqual(DateTime.Parse("2014-07-28T03:25:53+0000"), gpx.trk[0].trkseg[0].trkpt.Last().time, "last point time 4");

      Assert.AreEqual(6, gpx.wpt.Count, "waypoints count");

      Console.WriteLine(trackable.MapObject.Data);

      //using (var db = new MapContext("Server=(localdb)\\v11.0;Initial Catalog=AngularMap;Integrated Security=true;MultipleActiveResultSets=true"))
      //{
      //  var map = new Map { Name = "Test map" };
      //  map.Trackables.Add(trackable);
      //  db.Maps.Add(map);
      //  db.SaveChanges();
      //}
    }


    private static string[] responses = new[] {
@"{
""response"":{
	""feedMessageResponse"":{
		""count"":27,
		""feed"":{
			""id"":""0TaMFkpZncVBgn5c70UcJgKAHbVt9rYYD"",
			""name"":""RAD Team Tracker"",
			""description"":""RAD Team Tracker"",
			""status"":""ACTIVE"",
			""usage"":0,
			""daysRange"":3,
			""detailedMessageShown"":true
			},
		""totalCount"":27,
		""activityCount"":0,
		""messages"":{
			""message"":[
				{
					""@clientUnixTime"":""0"",
					""id"":123332526,
					""messengerId"":""0-1111111"",
					""messengerName"":""Test Tracker"",
					""unixTime"":1406422578,
					""messageType"":""TRACK"",
					""latitude"":47.3911,
					""longitude"":-121.51744,
					""modelId"":""SPOT2"",
					""showCustomMsg"":""Y"",
					""dateTime"":""2014-07-27T00:56:18+0000"",
					""batteryState"":""GOOD"",
					""hidden"":0
				},
				{""@clientUnixTime"":""0"",""id"":123330266,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406421987,""messageType"":""TRACK"",""latitude"":47.40252,""longitude"":-121.57158,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:46:27+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123328694,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406421423,""messageType"":""TRACK"",""latitude"":47.44304,""longitude"":-121.66364,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:37:03+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123326514,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406420785,""messageType"":""TRACK"",""latitude"":47.49404,""longitude"":-121.78351,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:26:25+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123322734,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406420168,""messageType"":""TRACK"",""latitude"":47.49416,""longitude"":-121.78378,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:16:08+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123320792,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406419565,""messageType"":""TRACK"",""latitude"":47.49409,""longitude"":-121.7836,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:06:05+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123318090,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406418969,""messageType"":""TRACK"",""latitude"":47.49422,""longitude"":-121.78362,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:56:09+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123315404,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406418370,""messageType"":""TRACK"",""latitude"":47.49417,""longitude"":-121.78349,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:46:10+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123315430,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406417770,""messageType"":""TRACK"",""latitude"":47.4407,""longitude"":-121.76289,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:36:10+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123315431,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406417170,""messageType"":""TRACK"",""latitude"":47.43482,""longitude"":-121.76864,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:26:10+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123296608,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406414173,""messageType"":""TRACK"",""latitude"":47.43442,""longitude"":-121.78265,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T22:36:13+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123293600,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406413574,""messageType"":""TRACK"",""latitude"":47.43438,""longitude"":-121.7827,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T22:26:14+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123291261,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406413085,""messageType"":""TRACK"",""latitude"":47.43393,""longitude"":-121.78413,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T22:18:05+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123280096,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406410841,""messageType"":""CUSTOM"",""latitude"":47.43468,""longitude"":-121.76871,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T21:40:41+0000"",""batteryState"":""GOOD"",""hidden"":0,""messageContent"":""RAD Team is responding to a possible mission. Check tracking page so see where we are.""},
				{""@clientUnixTime"":""0"",""id"":123265242,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406407973,""messageType"":""TRACK"",""latitude"":47.48728,""longitude"":-121.75856,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:52:53+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123265284,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406407373,""messageType"":""TRACK"",""latitude"":47.49496,""longitude"":-121.78585,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:42:53+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123259586,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406406851,""messageType"":""TRACK"",""latitude"":47.48709,""longitude"":-121.79195,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:34:11+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123256505,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406406331,""messageType"":""TRACK"",""latitude"":47.50615,""longitude"":-121.90157,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:25:31+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123252815,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406405615,""messageType"":""TRACK"",""latitude"":47.5298,""longitude"":-121.99471,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:13:35+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123252857,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406405015,""messageType"":""TRACK"",""latitude"":47.5298,""longitude"":-121.99514,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:03:35+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123252859,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406404415,""messageType"":""TRACK"",""latitude"":47.5298,""longitude"":-121.99514,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:53:35+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123243852,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406403775,""messageType"":""TRACK"",""latitude"":47.52977,""longitude"":-121.99509,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:42:55+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123239495,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406403175,""messageType"":""TRACK"",""latitude"":47.52976,""longitude"":-121.99509,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:32:55+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123239534,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406402575,""messageType"":""TRACK"",""latitude"":47.52985,""longitude"":-121.99509,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:22:55+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{
					""@clientUnixTime"":""0"",
					""id"":123232106,
					""messengerId"":""0-1111111"",
					""messengerName"":""Test Tracker"",
					""unixTime"":1406401841,
					""messageType"":""OK"",
					""latitude"":47.53015,
					""longitude"":-122.03773,
					""modelId"":""SPOT2"",
					""showCustomMsg"":""Y"",
					""dateTime"":""2014-07-26T19:10:41+0000"",
					""batteryState"":""GOOD"",
					""hidden"":0,
					""messageContent"":""RAD is active! Use the link in email to see where we are.\n""
				},
				{""@clientUnixTime"":""0"",""id"":123230598,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406401562,""messageType"":""TRACK"",""latitude"":47.53225,""longitude"":-122.04439,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:06:02+0000"",""batteryState"":""GOOD"",""hidden"":0},
				{""@clientUnixTime"":""0"",""id"":123227196,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406401001,""messageType"":""TRACK"",""latitude"":47.53226,""longitude"":-122.04443,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T18:56:41+0000"",""batteryState"":""GOOD"",""hidden"":0}
			]}
		}
	}
}",

@"{""response"":{""feedMessageResponse"":{""count"":36,""feed"":{""id"":""0TaMFkpZncVBgn5c70UcJgKAHbVt9rYYD"",""name"":""RAD Team Tracker"",""description"":""RAD Team Tracker"",""status"":""ACTIVE"",""usage"":0,""daysRange"":3,""detailedMessageShown"":true},""totalCount"":36,""activityCount"":0,""messages"":{""message"":[
{""@clientUnixTime"":""0"",""id"":123353259,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406428560,""messageType"":""TRACK"",""latitude"":47.47933,""longitude"":-121.78711,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:36:00+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123353280,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406427960,""messageType"":""TRACK"",""latitude"":47.43736,""longitude"":-121.66042,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:26:00+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123350291,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406427586,""messageType"":""TRACK"",""latitude"":47.42314,""longitude"":-121.62106,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:19:46+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123347817,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406426873,""messageType"":""TRACK"",""latitude"":47.43135,""longitude"":-121.63229,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:07:53+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123345558,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406426201,""messageType"":""TRACK"",""latitude"":47.39649,""longitude"":-121.53482,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:56:41+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123344289,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406425782,""messageType"":""TRACK"",""latitude"":47.39803,""longitude"":-121.46156,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:49:42+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123339094,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406424362,""messageType"":""TRACK"",""latitude"":47.42108,""longitude"":-121.41176,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:26:02+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123336914,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406423764,""messageType"":""TRACK"",""latitude"":47.39267,""longitude"":-121.47429,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:16:04+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123336940,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406423164,""messageType"":""TRACK"",""latitude"":47.39301,""longitude"":-121.4815,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:06:04+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123332526,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406422578,""messageType"":""TRACK"",""latitude"":47.3911,""longitude"":-121.51744,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:56:18+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123330266,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406421987,""messageType"":""TRACK"",""latitude"":47.40252,""longitude"":-121.57158,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:46:27+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123328694,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406421423,""messageType"":""TRACK"",""latitude"":47.44304,""longitude"":-121.66364,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:37:03+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123326514,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406420785,""messageType"":""TRACK"",""latitude"":47.49404,""longitude"":-121.78351,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:26:25+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123322734,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406420168,""messageType"":""TRACK"",""latitude"":47.49416,""longitude"":-121.78378,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:16:08+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123320792,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406419565,""messageType"":""TRACK"",""latitude"":47.49409,""longitude"":-121.7836,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:06:05+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123318090,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406418969,""messageType"":""TRACK"",""latitude"":47.49422,""longitude"":-121.78362,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:56:09+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123315404,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406418370,""messageType"":""TRACK"",""latitude"":47.49417,""longitude"":-121.78349,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:46:10+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123315430,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406417770,""messageType"":""TRACK"",""latitude"":47.4407,""longitude"":-121.76289,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:36:10+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123315431,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406417170,""messageType"":""TRACK"",""latitude"":47.43482,""longitude"":-121.76864,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:26:10+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123296608,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406414173,""messageType"":""TRACK"",""latitude"":47.43442,""longitude"":-121.78265,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T22:36:13+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123293600,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406413574,""messageType"":""TRACK"",""latitude"":47.43438,""longitude"":-121.7827,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T22:26:14+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123291261,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406413085,""messageType"":""TRACK"",""latitude"":47.43393,""longitude"":-121.78413,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T22:18:05+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123280096,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406410841,""messageType"":""CUSTOM"",""latitude"":47.43468,""longitude"":-121.76871,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T21:40:41+0000"",""batteryState"":""GOOD"",""hidden"":0,""messageContent"":""RAD Team is responding to a possible mission. Check tracking page so see where we are.""},
{""@clientUnixTime"":""0"",""id"":123265242,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406407973,""messageType"":""TRACK"",""latitude"":47.48728,""longitude"":-121.75856,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:52:53+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123265284,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406407373,""messageType"":""TRACK"",""latitude"":47.49496,""longitude"":-121.78585,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:42:53+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123259586,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406406851,""messageType"":""TRACK"",""latitude"":47.48709,""longitude"":-121.79195,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:34:11+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123256505,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406406331,""messageType"":""TRACK"",""latitude"":47.50615,""longitude"":-121.90157,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:25:31+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123252815,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406405615,""messageType"":""TRACK"",""latitude"":47.5298,""longitude"":-121.99471,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:13:35+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123252857,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406405015,""messageType"":""TRACK"",""latitude"":47.5298,""longitude"":-121.99514,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:03:35+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123252859,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406404415,""messageType"":""TRACK"",""latitude"":47.5298,""longitude"":-121.99514,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:53:35+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123243852,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406403775,""messageType"":""TRACK"",""latitude"":47.52977,""longitude"":-121.99509,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:42:55+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123239495,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406403175,""messageType"":""TRACK"",""latitude"":47.52976,""longitude"":-121.99509,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:32:55+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123239534,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406402575,""messageType"":""TRACK"",""latitude"":47.52985,""longitude"":-121.99509,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:22:55+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123232106,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406401841,""messageType"":""OK"",""latitude"":47.53015,""longitude"":-122.03773,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:10:41+0000"",""batteryState"":""GOOD"",""hidden"":0,""messageContent"":""RAD is active! Use the link in email to see where we are.\n""},
{""@clientUnixTime"":""0"",""id"":123230598,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406401562,""messageType"":""TRACK"",""latitude"":47.53225,""longitude"":-122.04439,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:06:02+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123227196,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406401001,""messageType"":""TRACK"",""latitude"":47.53226,""longitude"":-122.04443,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T18:56:41+0000"",""batteryState"":""GOOD"",""hidden"":0}]}}}}
",

@"{""response"":{""feedMessageResponse"":{""count"":50,""feed"":{""id"":""0TaMFkpZncVBgn5c70UcJgKAHbVt9rYYD"",""name"":""RAD Team Tracker"",""description"":""RAD Team Tracker"",""status"":""ACTIVE"",""usage"":0,""daysRange"":3,""detailedMessageShown"":true},""totalCount"":52,""activityCount"":0,""messages"":{""message"":[
{""@clientUnixTime"":""0"",""id"":123622505,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406502041,""messageType"":""OK"",""latitude"":47.41547,""longitude"":-121.51366,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T23:00:41+0000"",""batteryState"":""GOOD"",""hidden"":0,""messageContent"":""RAD is active! Use the link in email to see where we are.\n""},
{""@clientUnixTime"":""0"",""id"":123599646,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406497019,""messageType"":""TRACK"",""latitude"":47.40094,""longitude"":-121.51897,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T21:36:59+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123589107,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406494772,""messageType"":""CUSTOM"",""latitude"":47.46806,""longitude"":-121.72549,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T20:59:32+0000"",""batteryState"":""GOOD"",""hidden"":0,""messageContent"":""RAD Team is responding to a possible mission. Check tracking page so see where we are.""},
{""@clientUnixTime"":""0"",""id"":123585642,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406494049,""messageType"":""TRACK"",""latitude"":47.47874,""longitude"":-121.78716,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T20:47:29+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123583242,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406493587,""messageType"":""TRACK"",""latitude"":47.52668,""longitude"":-121.93932,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T20:39:47+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123579821,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406492850,""messageType"":""TRACK"",""latitude"":47.52941,""longitude"":-121.99579,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T20:27:30+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123577089,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406492275,""messageType"":""TRACK"",""latitude"":47.52986,""longitude"":-121.99568,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T20:17:55+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123574123,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406491657,""messageType"":""TRACK"",""latitude"":47.53011,""longitude"":-121.99557,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T20:07:37+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123571133,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406491055,""messageType"":""TRACK"",""latitude"":47.52964,""longitude"":-121.99583,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T19:57:35+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123568186,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406490450,""messageType"":""TRACK"",""latitude"":47.52979,""longitude"":-121.99564,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T19:47:30+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123568203,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406489850,""messageType"":""TRACK"",""latitude"":47.53052,""longitude"":-121.99444,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T19:37:30+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123563199,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406489460,""messageType"":""TRACK"",""latitude"":47.53006,""longitude"":-122.03127,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T19:31:00+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123559058,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406488596,""messageType"":""OK"",""latitude"":47.53227,""longitude"":-122.04446,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T19:16:36+0000"",""batteryState"":""GOOD"",""hidden"":0,""messageContent"":""RAD is active! Use the link in email to see where we are.\n""},
{""@clientUnixTime"":""0"",""id"":123358988,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406430358,""messageType"":""TRACK"",""latitude"":47.53019,""longitude"":-122.04056,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T03:05:58+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123357195,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406429786,""messageType"":""TRACK"",""latitude"":47.53821,""longitude"":-122.03639,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:56:26+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123355356,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406429166,""messageType"":""TRACK"",""latitude"":47.53024,""longitude"":-121.98372,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:46:06+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123353259,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406428560,""messageType"":""TRACK"",""latitude"":47.47933,""longitude"":-121.78711,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:36:00+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123353280,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406427960,""messageType"":""TRACK"",""latitude"":47.43736,""longitude"":-121.66042,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:26:00+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123350291,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406427586,""messageType"":""TRACK"",""latitude"":47.42314,""longitude"":-121.62106,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:19:46+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123347817,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406426873,""messageType"":""TRACK"",""latitude"":47.43135,""longitude"":-121.63229,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:07:53+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123345558,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406426201,""messageType"":""TRACK"",""latitude"":47.39649,""longitude"":-121.53482,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:56:41+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123344289,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406425782,""messageType"":""TRACK"",""latitude"":47.39803,""longitude"":-121.46156,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:49:42+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123339094,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406424362,""messageType"":""TRACK"",""latitude"":47.42108,""longitude"":-121.41176,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:26:02+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123336914,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406423764,""messageType"":""TRACK"",""latitude"":47.39267,""longitude"":-121.47429,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:16:04+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123336940,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406423164,""messageType"":""TRACK"",""latitude"":47.39301,""longitude"":-121.4815,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:06:04+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123332526,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406422578,""messageType"":""TRACK"",""latitude"":47.3911,""longitude"":-121.51744,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:56:18+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123330266,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406421987,""messageType"":""TRACK"",""latitude"":47.40252,""longitude"":-121.57158,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:46:27+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123328694,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406421423,""messageType"":""TRACK"",""latitude"":47.44304,""longitude"":-121.66364,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:37:03+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123326514,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406420785,""messageType"":""TRACK"",""latitude"":47.49404,""longitude"":-121.78351,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:26:25+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123322734,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406420168,""messageType"":""TRACK"",""latitude"":47.49416,""longitude"":-121.78378,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:16:08+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123320792,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406419565,""messageType"":""TRACK"",""latitude"":47.49409,""longitude"":-121.7836,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:06:05+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123318090,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406418969,""messageType"":""TRACK"",""latitude"":47.49422,""longitude"":-121.78362,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:56:09+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123315404,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406418370,""messageType"":""TRACK"",""latitude"":47.49417,""longitude"":-121.78349,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:46:10+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123315430,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406417770,""messageType"":""TRACK"",""latitude"":47.4407,""longitude"":-121.76289,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:36:10+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123315431,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406417170,""messageType"":""TRACK"",""latitude"":47.43482,""longitude"":-121.76864,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:26:10+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123296608,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406414173,""messageType"":""TRACK"",""latitude"":47.43442,""longitude"":-121.78265,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T22:36:13+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123293600,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406413574,""messageType"":""TRACK"",""latitude"":47.43438,""longitude"":-121.7827,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T22:26:14+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123291261,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406413085,""messageType"":""TRACK"",""latitude"":47.43393,""longitude"":-121.78413,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T22:18:05+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123280096,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406410841,""messageType"":""CUSTOM"",""latitude"":47.43468,""longitude"":-121.76871,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T21:40:41+0000"",""batteryState"":""GOOD"",""hidden"":0,""messageContent"":""RAD Team is responding to a possible mission. Check tracking page so see where we are.""},
{""@clientUnixTime"":""0"",""id"":123265242,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406407973,""messageType"":""TRACK"",""latitude"":47.48728,""longitude"":-121.75856,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:52:53+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123265284,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406407373,""messageType"":""TRACK"",""latitude"":47.49496,""longitude"":-121.78585,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:42:53+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123259586,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406406851,""messageType"":""TRACK"",""latitude"":47.48709,""longitude"":-121.79195,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:34:11+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123256505,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406406331,""messageType"":""TRACK"",""latitude"":47.50615,""longitude"":-121.90157,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:25:31+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123252815,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406405615,""messageType"":""TRACK"",""latitude"":47.5298,""longitude"":-121.99471,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:13:35+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123252857,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406405015,""messageType"":""TRACK"",""latitude"":47.5298,""longitude"":-121.99514,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T20:03:35+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123252859,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406404415,""messageType"":""TRACK"",""latitude"":47.5298,""longitude"":-121.99514,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:53:35+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123243852,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406403775,""messageType"":""TRACK"",""latitude"":47.52977,""longitude"":-121.99509,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:42:55+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123239495,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406403175,""messageType"":""TRACK"",""latitude"":47.52976,""longitude"":-121.99509,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:32:55+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123239534,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406402575,""messageType"":""TRACK"",""latitude"":47.52985,""longitude"":-121.99509,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:22:55+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123232106,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406401841,""messageType"":""OK"",""latitude"":47.53015,""longitude"":-122.03773,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T19:10:41+0000"",""batteryState"":""GOOD"",""hidden"":0,""messageContent"":""RAD is active! Use the link in email to see where we are.\n""}]}}}}
",

 @"{""response"":{""feedMessageResponse"":{""count"":50,""feed"":{""id"":""0TaMFkpZncVBgn5c70UcJgKAHbVt9rYYD"",""name"":""RAD Team Tracker"",""description"":""RAD Team Tracker"",""status"":""ACTIVE"",""usage"":0,""daysRange"":3,""detailedMessageShown"":true},""totalCount"":63,""activityCount"":0,""messages"":{""message"":[
{""@clientUnixTime"":""0"",""id"":123678499,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406517953,""messageType"":""TRACK"",""latitude"":47.48734,""longitude"":-121.79214,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-28T03:25:53+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123663072,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406512786,""messageType"":""TRACK"",""latitude"":47.40021,""longitude"":-121.51643,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-28T01:59:46+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123657058,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406510987,""messageType"":""TRACK"",""latitude"":47.40607,""longitude"":-121.5152,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-28T01:29:47+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123657078,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406510387,""messageType"":""TRACK"",""latitude"":47.40882,""longitude"":-121.5134,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-28T01:19:47+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123657079,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406509787,""messageType"":""TRACK"",""latitude"":47.41071,""longitude"":-121.51391,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-28T01:09:47+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123645627,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406507772,""messageType"":""TRACK"",""latitude"":47.41397,""longitude"":-121.51625,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-28T00:36:12+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123643413,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406507166,""messageType"":""TRACK"",""latitude"":47.41393,""longitude"":-121.51608,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-28T00:26:06+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123643419,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406506566,""messageType"":""TRACK"",""latitude"":47.41423,""longitude"":-121.51608,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-28T00:16:06+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123643420,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406505966,""messageType"":""TRACK"",""latitude"":47.41406,""longitude"":-121.51557,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-28T00:06:06+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123629175,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406503581,""messageType"":""TRACK"",""latitude"":47.41576,""longitude"":-121.51343,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T23:26:21+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123629190,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406502981,""messageType"":""TRACK"",""latitude"":47.41572,""longitude"":-121.51377,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T23:16:21+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123622505,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406502041,""messageType"":""OK"",""latitude"":47.41547,""longitude"":-121.51366,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T23:00:41+0000"",""batteryState"":""GOOD"",""hidden"":0,""messageContent"":""RAD is active! Use the link in email to see where we are.\n""},
{""@clientUnixTime"":""0"",""id"":123599646,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406497019,""messageType"":""TRACK"",""latitude"":47.40094,""longitude"":-121.51897,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T21:36:59+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123589107,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406494772,""messageType"":""CUSTOM"",""latitude"":47.46806,""longitude"":-121.72549,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T20:59:32+0000"",""batteryState"":""GOOD"",""hidden"":0,""messageContent"":""RAD Team is responding to a possible mission. Check tracking page so see where we are.""},
{""@clientUnixTime"":""0"",""id"":123585642,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406494049,""messageType"":""TRACK"",""latitude"":47.47874,""longitude"":-121.78716,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T20:47:29+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123583242,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406493587,""messageType"":""TRACK"",""latitude"":47.52668,""longitude"":-121.93932,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T20:39:47+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123579821,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406492850,""messageType"":""TRACK"",""latitude"":47.52941,""longitude"":-121.99579,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T20:27:30+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123577089,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406492275,""messageType"":""TRACK"",""latitude"":47.52986,""longitude"":-121.99568,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T20:17:55+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123574123,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406491657,""messageType"":""TRACK"",""latitude"":47.53011,""longitude"":-121.99557,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T20:07:37+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123571133,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406491055,""messageType"":""TRACK"",""latitude"":47.52964,""longitude"":-121.99583,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T19:57:35+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123568186,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406490450,""messageType"":""TRACK"",""latitude"":47.52979,""longitude"":-121.99564,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T19:47:30+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123568203,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406489850,""messageType"":""TRACK"",""latitude"":47.53052,""longitude"":-121.99444,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T19:37:30+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123563199,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406489460,""messageType"":""TRACK"",""latitude"":47.53006,""longitude"":-122.03127,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T19:31:00+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123559058,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406488596,""messageType"":""OK"",""latitude"":47.53227,""longitude"":-122.04446,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T19:16:36+0000"",""batteryState"":""GOOD"",""hidden"":0,""messageContent"":""RAD is active! Use the link in email to see where we are.\n""},
{""@clientUnixTime"":""0"",""id"":123358988,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406430358,""messageType"":""TRACK"",""latitude"":47.53019,""longitude"":-122.04056,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T03:05:58+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123357195,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406429786,""messageType"":""TRACK"",""latitude"":47.53821,""longitude"":-122.03639,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:56:26+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123355356,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406429166,""messageType"":""TRACK"",""latitude"":47.53024,""longitude"":-121.98372,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:46:06+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123353259,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406428560,""messageType"":""TRACK"",""latitude"":47.47933,""longitude"":-121.78711,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:36:00+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123353280,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406427960,""messageType"":""TRACK"",""latitude"":47.43736,""longitude"":-121.66042,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:26:00+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123350291,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406427586,""messageType"":""TRACK"",""latitude"":47.42314,""longitude"":-121.62106,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:19:46+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123347817,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406426873,""messageType"":""TRACK"",""latitude"":47.43135,""longitude"":-121.63229,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T02:07:53+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123345558,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406426201,""messageType"":""TRACK"",""latitude"":47.39649,""longitude"":-121.53482,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:56:41+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123344289,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406425782,""messageType"":""TRACK"",""latitude"":47.39803,""longitude"":-121.46156,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:49:42+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123339094,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406424362,""messageType"":""TRACK"",""latitude"":47.42108,""longitude"":-121.41176,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:26:02+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123336914,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406423764,""messageType"":""TRACK"",""latitude"":47.39267,""longitude"":-121.47429,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:16:04+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123336940,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406423164,""messageType"":""TRACK"",""latitude"":47.39301,""longitude"":-121.4815,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T01:06:04+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123332526,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406422578,""messageType"":""TRACK"",""latitude"":47.3911,""longitude"":-121.51744,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:56:18+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123330266,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406421987,""messageType"":""TRACK"",""latitude"":47.40252,""longitude"":-121.57158,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:46:27+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123328694,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406421423,""messageType"":""TRACK"",""latitude"":47.44304,""longitude"":-121.66364,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:37:03+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123326514,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406420785,""messageType"":""TRACK"",""latitude"":47.49404,""longitude"":-121.78351,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:26:25+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123322734,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406420168,""messageType"":""TRACK"",""latitude"":47.49416,""longitude"":-121.78378,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:16:08+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123320792,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406419565,""messageType"":""TRACK"",""latitude"":47.49409,""longitude"":-121.7836,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-27T00:06:05+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123318090,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406418969,""messageType"":""TRACK"",""latitude"":47.49422,""longitude"":-121.78362,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:56:09+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123315404,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406418370,""messageType"":""TRACK"",""latitude"":47.49417,""longitude"":-121.78349,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:46:10+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123315430,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406417770,""messageType"":""TRACK"",""latitude"":47.4407,""longitude"":-121.76289,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:36:10+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123315431,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406417170,""messageType"":""TRACK"",""latitude"":47.43482,""longitude"":-121.76864,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T23:26:10+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123296608,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406414173,""messageType"":""TRACK"",""latitude"":47.43442,""longitude"":-121.78265,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T22:36:13+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123293600,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406413574,""messageType"":""TRACK"",""latitude"":47.43438,""longitude"":-121.7827,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T22:26:14+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123291261,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406413085,""messageType"":""TRACK"",""latitude"":47.43393,""longitude"":-121.78413,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T22:18:05+0000"",""batteryState"":""GOOD"",""hidden"":0},
{""@clientUnixTime"":""0"",""id"":123280096,""messengerId"":""0-1111111"",""messengerName"":""Test Tracker"",""unixTime"":1406410841,""messageType"":""CUSTOM"",""latitude"":47.43468,""longitude"":-121.76871,""modelId"":""SPOT2"",""showCustomMsg"":""Y"",""dateTime"":""2014-07-26T21:40:41+0000"",""batteryState"":""GOOD"",""hidden"":0,""messageContent"":""RAD Team is responding to a possible mission. Check tracking page so see where we are.""}]}}}}
" };

  }

  public class TestSpotTracker : SpotTracker
  {
    public string NextResponse { get; set; }

    protected override string DownloadFeed()
    {
      return this.NextResponse;
    }
  }
}
