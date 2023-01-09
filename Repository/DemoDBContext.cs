using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Entities;

namespace Repository
{
    public class DemoDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DemoDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = Configuration.GetConnectionString("DemoDB");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<AdminLogin> AdminLogins { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<NotJoinStudyTime> NotJoinStudyTimes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentLogin> StudentLogins { get; set; }
        public DbSet<StudentTest> StudentTests { get; set; }
        public DbSet<StudyTime> StudyTimes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherLogin> TeacherLogins { get; set; }
        public DbSet<Test> Tests { get; set; }

    }
}

