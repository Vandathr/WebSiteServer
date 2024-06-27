using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;
using System.ComponentModel;
using System.Windows.Forms;

namespace WebSiteServer
{
    public class Model: IReceiver
    {        
        private readonly IServer Server;
        private readonly BindingList<string> messageN = new BindingList<string>();


        public void LaunchServer() => Server.LaunchServer();
        public void CeaseCerver() => Server.CeaseCerver();
        public void ReloadData() => Server.ReloadData();


        public BindingList<string> GetMessagezList() => messageN;

        public void SendMessage(string message)
        {
            if (messageN.Count > 100) messageN.Clear();

            messageN.Add(message);
        }


        public Model()
        {
            Server = new ServerHTTP(this);
        }      
    }
}
