using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace Trackables
{
  interface ITracker
  {
    string Poll(MapTrackable mapObject);
  }
}
