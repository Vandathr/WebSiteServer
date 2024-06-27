using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WebSiteServer
{
    internal class CommandzExtractor
    {
        private readonly string determiner;

        private string[] rowN;
        private string[] commandN;

        public bool ExtractCommandz(ImagezProcessor Processor, string[] elementN)
        {
            rowN = elementN[1].Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            if (rowN.Length < 2) return false;
            if (rowN[0] != determiner) return false;

            commandN = rowN[1].Split(',');

            foreach (var e in commandN)
                if(e != "") Processor.AddCommand(int.Parse(e));

            return true;
        }


        public CommandzExtractor(string determiner) => this.determiner = determiner;

    }
}
