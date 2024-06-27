using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteServer
{
    internal interface IServer
    {
        void LaunchServer();
        void CeaseCerver();
        void ReloadData();
    }
}
