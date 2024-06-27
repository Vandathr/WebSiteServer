using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace WebSiteServer
{
    internal class DataLoader : IEnumerable<string>
    {
        private readonly string pathMain = @"\MAIN.txt";
        private readonly FilezKeeper[] keeperN; 

        private StreamReader Reader;
        private int activeContainer = 0;

        private string[] fileHTMLN;
        private string[] fileCSSN;
        private string[] fileJavaScriptN;


        public void SetActiveContainer(int indexOfContainer) => activeContainer = indexOfContainer;
        public IEnumerator<string> GetEnumerator() => keeperN[activeContainer].GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public void ReloadData()
        {
            if (fileHTMLN != null && fileCSSN != null)
            {
                keeperN[htmlIndex].LoadFilez(fileHTMLN);
                keeperN[cssIndex].LoadFilez(fileCSSN);
                keeperN[javaScriptIndex].LoadFilez(fileJavaScriptN);
            }
        }

        public int GetLength() => keeperN.Length;

        public DataLoader()
        {

            keeperN = new FilezKeeper[]{ new FilezKeeper("html"), new FilezKeeper("css"), new FilezKeeper("JavaScript") };

            Reader = new StreamReader(Environment.CurrentDirectory + pathMain);

            fileHTMLN = Directory.GetFiles(Environment.CurrentDirectory + Reader.ReadLine());
            fileCSSN = Directory.GetFiles(Environment.CurrentDirectory + Reader.ReadLine());
            fileJavaScriptN = Directory.GetFiles(Environment.CurrentDirectory + Reader.ReadLine());

            Reader.Close();

            keeperN[htmlIndex].LoadFilez(fileHTMLN);
            keeperN[cssIndex].LoadFilez(fileCSSN);
            keeperN[javaScriptIndex].LoadFilez(fileJavaScriptN);
        }


        public byte[] this[string name] => keeperN[activeContainer][name];


        public readonly static int htmlIndex = 0;
        public readonly static int cssIndex = 1;
        public readonly static int javaScriptIndex = 2;

    }
}
