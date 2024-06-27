using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteServer
{
    internal static class Positioner
    {
        public static void LocateControl(Control ControlToLocate, Size SizeOfControl, Point Location)
        {
            ControlToLocate.Size = SizeOfControl;
            ControlToLocate.Location = Location;
        }
    }
}
