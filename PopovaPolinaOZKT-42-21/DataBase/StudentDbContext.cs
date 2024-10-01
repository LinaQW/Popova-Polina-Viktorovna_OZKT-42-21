using Microsoft.EntityFrameworkCore;
using PopovaPolinaOZKT_42_21.DataBase.Configurations;
using PopovaPolinaOZKT_42_21.DataBase.Models;



namespace PopovaPolinaOZKT_42_21.DataBase
{
    public class StudentDbContext : DbContext
    {
        //Добавляем таблицы
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Добавляем конфигурации к таблицам
            modelBuilder.ApplyConfiguration(new ExamConfiguration());
            modelBuilder.ApplyConfiguration(new GradeConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());

        }

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }
        
    }
}
