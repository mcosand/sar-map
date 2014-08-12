using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
  public class MapTrackableUpdate
  {
    public DateTime Time { get; set; }
    public decimal Lat { get; set; }
    public decimal Lng { get; set; }
    public string Message { get; set; }
    public string MessageType { get; set; }
  }
}
