using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.Proxy
{
    public class ProxyImage : IFacePicture
    {
        public RealImage RealImage { get; set; }

        public void ShowImage(int id)
        {
            RealImage = new RealImage(id);
            RealImage.ShowImage(id);
        }
    }
}