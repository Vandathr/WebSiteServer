using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteServer
{
    internal class OptionzExtractor
    {
        private readonly string determiner;

        private string[] rowN;
        private string[] commandN;


        public bool ExtractOptionz(ImagezProcessor Processor, string[] elementN)
        {
            rowN = elementN[2].Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            if (rowN.Length < 2) return false;
            if (rowN[0] != determiner) return false;

            commandN = rowN[1].Split(',');

            //Processor.SetLowerScope(double.Parse(commandN[0], CultureInfo.InvariantCulture));
            //Processor.SetUpperScope(double.Parse(commandN[1], CultureInfo.InvariantCulture));

            return true;
        }



        public OptionzExtractor(string determiner) => this.determiner = determiner;
    }
}
