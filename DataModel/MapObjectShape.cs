using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
  public class MapObjectShape
  {
    public Guid Id { get; set; }
    
    public Guid OwnerId { get; set; }

    public MapObject Owner { get; set; }

    public DbGeography Shape { get; set; }
  }
}
