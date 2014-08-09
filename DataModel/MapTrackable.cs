using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
  }

  public class TrackableUpdateException : ApplicationException
  {
    public TrackableUpdateException(string message, Exception inner)
      : base(message, inner)
    {
    }
  }
}
