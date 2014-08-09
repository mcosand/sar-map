using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
  public class MapContext : DbContext
  {
    public MapContext()
      : base()
    {
    }

    public MapContext(string conn): base(conn)
    {
    }

    public IDbSet<Map> Maps { get; set; }
    public IDbSet<MapTrackable> Trackables { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<MapObject>().HasOptional(f => f.Parent).WithMany(f => f.Children).WillCascadeOnDelete(false);
    }
  }
}
