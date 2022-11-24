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
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.ListCourse)
                .HasForeignKey(c=>c.TeacherID);
        }

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

