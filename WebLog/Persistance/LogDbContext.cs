using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Security.Cryptography;
using WebLog.Core.TemplateMethod;

namespace WebLog.Core.Models
{
    public class SchoolDbInitializer : CreateDatabaseIfNotExists<LogDbContext>
    {
        protected override void Seed(LogDbContext context)
        {
            context.Admin.Add(new Admin("admin", "admin", "admin@vp.pl", "password", "95092701812"));
            var s1 = new Student("Damian", "Cyzar", "s1@vp.pl", "password", "11111111111");
            var s2 = new Student("Jan", "Nowak", "s2@vp.pl", "password", "22222222222");
            var s3 = new Student("Tomek", "Kajko", "s3@vp.pl", "password", "33333333333");
            context.Students.Add(s1);
            context.Students.Add(s2);
            context.Students.Add(s3);
            var t1 = new Teacher("Kuba", "Kowal", "t1@vp.pl", "password", "12312312311");
            var t2 = new Teacher("Marcin", "Nowakowski", "t2@vp.pl", "password", "32132132122");
            context.Teachers.Add(t1);
            context.Teachers.Add(t2);
            var p1 = new Parent("Kamil", "Cyzar", "kamelek2@vp.pl", "password", "44444444444");
            var p2 = new Parent("Witold", "Nowak", "czesio476@gmail.com", "password", "55555555555");
            p1.MyChildrens.Add(s1);
            p2.MyChildrens.Add(s2);
            context.Parents.Add(p1);
            context.Parents.Add(p2);
            context.Subjects.Add(new Subject("Matematyka", @"https://data1.cupsell.pl/upload/generator/51436/640x420/1089545_print-trimmed-1.png?resize=max_sizes&key=55f9a22768eed085006592c1174c0235"));
            context.Subjects.Add(new Subject("Angielski", @"http://translatica.pl/theme/Dictionary/img/en.svg?1410243971"));
            context.Subjects.Add(new Subject("Biologia", @"http://www.vitalogy.pl/photo_max/biologia.jpg"));
            context.Subjects.Add(new Subject("Polski", @"http://www.newsweek.pl/g/i.aspx/680/0/newsweek/635521994225190164.jpg"));
            context.Subjects.Add(new Subject("Chemia", @"http://www.placpigal.pl/blog/wp-content/uploads/2015/12/chemia1.jpg"));
            context.Subjects.Add(new Subject("Geografia", @"http://lo.krapkowice.pl/download//388/geografia.jpeg"));
            base.Seed(context);
            context.SaveChanges();
        }
    }

    //public class DatabaseConfiguration : DbMigrationsConfiguration<LogDbContext>
    //{
    //    public DatabaseConfiguration()
    //    {
    //        this.AutomaticMigrationsEnabled = true;
    //    }
    //}

    public class LogDbContext : DbContext
    {
        public LogDbContext() : base("LogWeb3")
        {
            Database.SetInitializer(new SchoolDbInitializer());
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; } 
        public DbSet<SchoolClass> SchoolClass { get; set; }
        public DbSet<MedicalClass> MedicalClass { get; set; }
        public DbSet<MathematicClass> MathematicClass { get; set; }
        public DbSet<LanguageClass> LanguageClass { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SchoolGrade> SchoolGrades { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<SubjectFile> Files { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Test> Tests { get; set; }
    }
}