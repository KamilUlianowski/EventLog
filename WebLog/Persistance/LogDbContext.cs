using System.Data.Entity;

namespace WebLog.Core.Models
{
    public class LogDbContext : DbContext
    {
        public LogDbContext() : base("LogWeb")
        {
            
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<SchoolClass> SchoolClass { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SchoolGrade> SchoolGrades { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}