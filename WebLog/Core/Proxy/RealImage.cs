using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.Repositories;
using WebLog.Persistance.Repositories;

namespace WebLog.Core.Proxy
{
    public class RealImage : IFacePicture
    {
        public ImageTeacher ImageTeacher { get; set; }

        public void ShowImage(int id)
        {

        }

        private void LoadImage(int id)
        {
           ITeacherRepository teacherRepository = new TeacherRepository(new LogDbContext());
           ImageTeacher = teacherRepository.GetTeacherImage(id);

        }

        public RealImage(int id)
        {
            LoadImage(id);
        }

    }
}