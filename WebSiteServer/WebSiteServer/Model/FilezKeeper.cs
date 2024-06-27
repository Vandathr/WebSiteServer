using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebSiteServer
{
    internal class FilezKeeper: IEnumerable<string>
    {
        private readonly string id;
        private readonly List<string> elementNameN = new List<string>();
        private readonly List<byte[]> elementBodyN = new List<byte[]>();

        private readonly string notFoundHTMLName = "NotFoundHTML.html";

        public void LoadFilez(string[] fileN)
        {
            elementNameN.Clear();
            elementBodyN.Clear();

            foreach (var e in fileN)
            {
                elementNameN.Add(Path.GetFileName(e));

                var Reader = new StreamReader(e);
                elementBodyN.Add(Encoding.UTF8.GetBytes(Reader.ReadToEnd()));
                Reader.Close();
            }

        }


        public void LoadAndAdvanceScript(string fileName)
        {
            elementNameN.Clear();
            elementBodyN.Clear();

            elementNameN.Add(Path.GetFileName(fileName));

            var Reader = new StreamReader(fileName);
            var scriptBody = Reader.ReadToEnd();


        }



        public string GetId() => id;
        public bool CompareId(string id) => this.id == id;



        public FilezKeeper(string id)
        {
            this.id = id;
        }



        private byte[] GetPageName(string name)
        {
            var indexOfPage = elementNameN.IndexOf(name);

            if (indexOfPage < 0)
                indexOfPage = elementNameN.IndexOf(notFoundHTMLName);

            return elementBodyN[indexOfPage];
        }

        public IEnumerator<string> GetEnumerator()
        {
            return elementNameN.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public byte[] this[string name] 
        {
            get => GetPageName(name);
        }




    }
}
