using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSiteServer
{
    internal class BodyReader
    {
        private StreamReader Reader;
        private string redBody;

        public string[] ReadBody(Stream ToRead, string boundary)
        {
            Reader = new StreamReader(ToRead);

            redBody = Reader.ReadToEnd();
            var elementN = redBody.Split(boundary, StringSplitOptions.RemoveEmptyEntries);

            Reader.Close();

            return elementN;
        }


    }
}
