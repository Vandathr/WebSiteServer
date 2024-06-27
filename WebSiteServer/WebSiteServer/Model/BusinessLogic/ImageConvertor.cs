using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteServer
{
    internal class ImageConvertor
    {
        private readonly string prefix;

        private MemoryStream byteReader;
        private byte[] imagebytezToProcess;



        public string ConvertImageToByte(Image ToConvert)
        {

            byteReader = new MemoryStream();
            ToConvert.Save(byteReader, ImageFormat.Png);
            imagebytezToProcess = byteReader.ToArray();

            return prefix + Convert.ToBase64String(imagebytezToProcess);
        }


        public ImageConvertor(string prefix) => this.prefix = prefix;

    }
}
