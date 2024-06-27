using ImagezProcessorzLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteServer
{
    internal class ImagezProcessor: ImagezProcessorzLibrary.ImagezMainProcessor
    {
        private readonly ImagezConvertor ConverterOfImagez = new ImagezConvertor();

        private Map Frame;


        public Bitmap Process(Image[] imageN)
        {
            int commandIndex;

            Frame = ConverterOfImagez.MakeMap((Bitmap)imageN[0]);

            while (base.commandzQueue.Count > 0)
            {
                commandIndex = commandzQueue.Dequeue();
                pixelProcessorN[commandIndex].DoWork(Frame);

            }

            return ConverterOfImagez.MakeImage(Frame);
        }

    }
}
