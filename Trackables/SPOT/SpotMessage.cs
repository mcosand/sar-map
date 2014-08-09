using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackables.SPOT
{
  public class SpotMessage
  {
    public string id { get; set; }
    public DateTime dateTime { get; set; }
    public string messageType { get; set; }
    public decimal latitude { get; set; }
    public decimal longitude { get; set; }
    public string messageContent { get; set; }
  }
}
