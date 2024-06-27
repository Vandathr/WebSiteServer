using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WebSiteServer
{
    internal class ImageExtractor
    {
        private readonly string determiner;

        private string[] rowN;
        private byte[] imagebytezToProcess;
        private MemoryStream byteReader;



        public bool ExtractImage(Image[] imageToProcessN, string[] elementN)
        {
            rowN = elementN[0].Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            if (rowN.Length < 2) return false;
            if (rowN[0] != determiner) return false;

            imagebytezToProcess = Convert.FromBase64String(rowN[1].Split(',')[1]);
            byteReader = new MemoryStream(imagebytezToProcess);

            imageToProcessN[0] = Image.FromStream(byteReader);

            return true;
        }


        public ImageExtractor(string determiner) => this.determiner = determiner;

    }
}
