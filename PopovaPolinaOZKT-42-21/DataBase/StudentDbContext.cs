using Microsoft.EntityFrameworkCore;
using PopovaPolinaOZKT_42_21.DataBase.Configurations;
using PopovaPolinaOZKT_42_21.DataBase.Models;
//using PopovaPolinaOZKT_42_21.Models;

namespace PopovaPolinaOZKT_42_21.DataBase
{
    public class StudentDbContext : DbContext
    {
        //Добавляем таблицы
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Добавляем конфигурации к таблицам
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
        }

        public StudentDbContext(DbContextOptions options) : base(options)
        {
        }
        
    }
}
