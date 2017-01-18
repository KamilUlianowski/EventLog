using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLog.Core.Models
{
    public class ImageTeacher
    {
        public int Id { get; set; }

        public byte[] Picture { get; set; }

        public ImageTeacher()
        {
            
        }

        public ImageTeacher(byte[] picture)
        {
            Picture = picture;
        }
    }
}