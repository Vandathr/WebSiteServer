using ImagezProcessorzLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteServer
{
    internal class BusinessLogic
    {
        private readonly ImagezProcessor Processor = new ImagezProcessor();
        private readonly BoundaryGetter GetterOfBoundary = new BoundaryGetter("boundary=");
        private readonly BodyReader ReaderOfBody = new BodyReader();
        private readonly CommandzExtractor ExtractorOfCommandz = new CommandzExtractor("Content-Disposition: form-data; name=\"commandzToPreprocessor\"");
        private readonly OptionzExtractor ExtractorOfOptionz = new OptionzExtractor("Content-Disposition: form-data; name=\"scopezValuezToPreprocessor\"");
        private readonly ImageExtractor ExtractorOfImage = new ImageExtractor("Content-Disposition: form-data; name=\"ImageToProcess\"");       
        private readonly ImageConvertor ConvertorOfImage = new ImageConvertor("data:image/png;base64,");

        private string[] elementN;
        private Image[] imageToProcessN = new Image[1];


        public string DoWork(HttpListenerContext Context)
        {
            var textBoundary = GetterOfBoundary.GetBoundary(Context.Request.ContentType);

            elementN = ReaderOfBody.ReadBody(Context.Request.InputStream, textBoundary);

            if (elementN.Length < 3) return "";

            bool IsCommandExtractionSuccess = ExtractorOfCommandz.ExtractCommandz(Processor, elementN);

            if (!IsCommandExtractionSuccess) return "";

            bool IsOptionzExtractionSuccess = ExtractorOfOptionz.ExtractOptionz(Processor, elementN);

            if (!IsOptionzExtractionSuccess) return "";

            ExtractorOfImage.ExtractImage(imageToProcessN, elementN);

            imageToProcessN[0] = Processor.Process(imageToProcessN);

            return ConvertorOfImage.ConvertImageToByte(imageToProcessN[0]);
        }

        public List<string> GetCommandzList()
        {
            List<string> toReturnN = new List<string>();

            foreach (var e in Processor)
                toReturnN.Add(((IImagezProcessor)e).ToString());

            return toReturnN;
        }



    }
}
