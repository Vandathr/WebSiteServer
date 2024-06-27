using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImagezProcessorzLibrary;

namespace WebSiteServer
{
    internal class ImagezConvertor
    {
        Color TempColor;
        RGB TempRGB;

        public RGB[,] ConvertToArray(Bitmap ToConvert)
        {
            var width = ToConvert.Width;
            var height = ToConvert.Height;


            RGB[,] ToReturn = new RGB[width, height];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    TempColor = ToConvert.GetPixel(i, j);
                    ToReturn[i, j] = new RGB(TempColor.R, TempColor.G, TempColor.B);
                }

            return ToReturn;
        }

        public Map MakeMap(Bitmap ToConvert)
        {
            var width = ToConvert.Width;
            var height = ToConvert.Height;

            Map ToReturn = new Map(width, height);

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    TempColor = ToConvert.GetPixel(i, j);
                    ToReturn[i, j] = new RGB(TempColor.R, TempColor.G, TempColor.B);
                }

            return ToReturn;
        }


        public Bitmap ConvertToImage(RGB[,] ToConvert)
        {
            var width = ToConvert.GetLength(0);
            var height = ToConvert.GetLength(1);


            Bitmap ToReturn = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    TempRGB = ToConvert[i, j];
                    ToReturn.SetPixel(i, j, Color.FromArgb((int)TempRGB.GetR(), (int)TempRGB.GetG(), (int)TempRGB.GetB()));
                }

            return ToReturn;
        }


        public Bitmap MakeImage(Map ToConvert)
        {
            var width = ToConvert.GetLength(0);
            var height = ToConvert.GetLength(1);


            Bitmap ToReturn = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    TempRGB = ToConvert[i, j];
                    ToReturn.SetPixel(i, j, Color.FromArgb((int)TempRGB.GetR(), (int)TempRGB.GetG(), (int)TempRGB.GetB()));
                }

            return ToReturn;
        }

    }
 }
