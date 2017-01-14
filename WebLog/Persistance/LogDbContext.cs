using System.Data.Entity;
using System.Security.Cryptography;

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
            base.Seed(context);
        }
    }

    public class LogDbContext : DbContext
    {
        public LogDbContext() : base("LogWeb3")
        {
            Database.SetInitializer(new SchoolDbInitializer());
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<SchoolClass> SchoolClass { get; set; }
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