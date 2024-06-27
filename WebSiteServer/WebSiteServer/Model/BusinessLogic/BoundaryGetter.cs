using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteServer
{
    internal class BoundaryGetter
    {
        private readonly string boundary;

        public string GetBoundary(string contentType) => "--" + contentType.Substring(contentType.IndexOf(boundary) + boundary.Length);
        

        public BoundaryGetter(string boundary) => this.boundary = boundary;
           
    }
}
