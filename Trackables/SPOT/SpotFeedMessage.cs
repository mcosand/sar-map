using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackables.SPOT
{
  public class SpotFeedMessage
  {
    public int count { get; set; }
    public SpotFeed feed { get; set; }
    public int totalCount { get; set; }
    public SpotMessages messages { get; set; }

  }
}
