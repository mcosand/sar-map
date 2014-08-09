using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataModel
{
  public abstract class GpxNode
  {
    protected readonly XmlElement xml;
    protected readonly XmlNamespaceManager ns;
    public GpxNode(XmlElement element, XmlNamespaceManager ns)
    {
      this.xml = element;
      this.ns = ns;
    }
  }

  public class GpxTrack : GpxNode
  {
    public GpxTrack(XmlElement node, XmlNamespaceManager ns)
      : base(node, ns)
    {
    }

    public string Name
    {
      get
      {
        return this.xml.SelectSingleNode("name").InnerText;
      }
      set
      {
        var nameNode = this.xml.SelectSingleNode("name");
        if (nameNode == null)
        {
          nameNode = this.xml.OwnerDocument.CreateElement("name");
        }
        nameNode.InnerText = value ?? string.Empty;
      }
    }

    public ObservableCollection<GpxTrackSegment> segments = null;
    public ObservableCollection<GpxTrackSegment> Segments
    {
      get
      {
        if (this.segments == null)
        {
          this.segments = new ObservableCollection<GpxTrackSegment>(
            this.xml.SelectNodes("g:trkseg", ns).OfType<XmlElement>()
              .Select(f => new GpxTrackSegment(f, ns)));
          this.segments.CollectionChanged += segments_CollectionChanged;
        }
        return this.segments;
      }
    }

    void segments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
 //     throw new NotImplementedException();
    }

    public GpxTrack AppendTrack(string name)
    {
      var segmentXml = this.xml.OwnerDocument.CreateElement(
        "trkseg",
        this.ns.LookupNamespace("g"));
      var track = new GpxTrack(segmentXml, this.ns);

      // use this order so that an uninitialized tracks list doesn't
      // initialize with the new node
      this.tracks.Add(track);
      this.xmlDoc.AppendChild(segmentXml);
      track.AppendSegment();
      return track;
    }
  }

  public class GpxTrackSegment : GpxNode
  {
    public GpxTrackSegment(XmlElement node, XmlNamespaceManager ns)
      : base(node, ns)
    {
    }

    public ObservableCollection<GpxTrackPoint> points = null;
    public ObservableCollection<GpxTrackPoint> Points
    {
      get
      {
        if (this.points == null)
        {
          this.points = new ObservableCollection<GpxTrackPoint>(
            this.xml.SelectNodes("g:trkpt").OfType<XmlElement>()
              .Select(f => new GpxTrackPoint(f, ns)));
          this.points.CollectionChanged += points_CollectionChanged;
        }
        return this.points;
      }
    }

    void points_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      throw new NotImplementedException();
    }

    public GpxTrackPoint AppendPoint(double latitude, double longitude)
    {
      XmlElement element = this.xml.OwnerDocument.CreateElement("g:trkpt");
      element.SetAttribute("lat", latitude.ToString());
      element.SetAttribute("lon", longitude.ToString());
      this.xml.AppendChild(element);
      return new GpxTrackPoint(element, ns);
    }
  }

  public class GpxTrackPoint : GpxNode
  {
    public GpxTrackPoint(XmlElement node, XmlNamespaceManager ns)
      : base(node, ns)
    {
    }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double? Elevation { get; set; }
    public DateTime? Time { get; set; }
  }

  public class GpxFile
  {
    private const string DefaultNamespace = "http://www.topografix.com/GPX/1/1";
    private readonly XmlDocument xmlDoc = new XmlDocument();
    private readonly XmlNamespaceManager ns;
    public GpxFile(string xml)
    {
      if (string.IsNullOrWhiteSpace(xml))
      {
        xml = @"<gpx xmlns=""" + DefaultNamespace + @""" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" version=""1.1"" xsi:schemaLocation=""http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd""></gpx>";
      }

      this.xmlDoc.LoadXml(xml);
      this.ns = new XmlNamespaceManager(this.xmlDoc.NameTable);
      this.ns.AddNamespace("g", "http://www.topografix.com/GPX/1/1");
    }

    private ObservableCollection<GpxTrack> tracks = null;
    public ObservableCollection<GpxTrack> Tracks
    {
      get
      {
        if (this.tracks == null)
        {
          this.tracks = new ObservableCollection<GpxTrack>(
            this.xmlDoc.SelectNodes("//g:trk", ns).OfType<XmlElement>()
              .Select(f => new GpxTrack(f, ns)));
          this.tracks.CollectionChanged += tracks_CollectionChanged;
        }
        return this.tracks;
      }
    }

    void tracks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      throw new NotImplementedException();
    }

    public string Xml { get { return this.xmlDoc.OuterXml; } }

    public GpxTrack AppendTrack(string name)
    {
      var trackXml = this.xmlDoc.CreateElement("trk", DefaultNamespace);
      var track = new GpxTrack(trackXml, this.ns);
      
      // use this order so that an uninitialized tracks list doesn't
      // initialize with the new node
      this.tracks.Add(track);
      this.xmlDoc.AppendChild(trackXml);
      track.AppendSegment();
      return track;
    }
  }
}
