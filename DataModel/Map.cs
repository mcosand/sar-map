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
    }

    public Guid Id { get; set; }
    public string Name { get; set; }

    public double Lat { get; set; }
    public double Lng { get; set; }
    public int Zoom { get; set; }
    public DateTime Created { get; set; }

    public ICollection<MapTrackable> Trackables { get; set; }
  }
}
