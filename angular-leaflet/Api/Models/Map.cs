using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace angular_leaflet.Api.Models
{
  public class Map
  {
    public Guid Id { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
    public int Zoom { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
  }
}