using System;
using System.Collections.Generic;
using System.Text;
using SUS.HTTP;
using SUS.MvcFramework;

namespace IRunes.Controllers
{
   public class AlbumsController:Controller
    {
        public HttpResponse All()
        {
            return this.View();
        }

        public HttpResponse Create()
        {
            return this.View();
        }

        public HttpResponse Details()
        {
            return this.View();
        }
    }
}
