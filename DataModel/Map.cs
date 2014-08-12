using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel
{
  public class Map
  {
    public Map()
    {
      this.Id = Guid.NewGuid();
      this.Trackables = new List<MapTrackable>();
      this.Created = DateTime.Now;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public double Lat { get; set; }
    public double Lng { get; set; }
    public int Zoom { get; set; }
    public DateTime Created { get; set; }

    public ICollection<MapTrackable> Trackables { get; set; }
    public ICollection<MapObject> Objects { get; set; }
  }
}
