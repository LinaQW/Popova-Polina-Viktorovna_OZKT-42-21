using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PopovaPolinaOZKT_42_21.DataBase.Helpers;
using PopovaPolinaOZKT_42_21.DataBase.Models;


namespace PopovaPolinaOZKT_42_21.DataBase.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        private const string TableName = "Grade";
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            //Задаем первичный ключ
            builder
                .HasKey(p => p.GradeId)
                .HasName($"pk_{TableName}_Id");

            //Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
            builder.Property(p => p.GradeId)
                    .ValueGeneratedOnAdd();

            //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.GradeId)
                .HasColumnName("Id")
                .HasComment("Идентификатор записи зачета");

            //HasComment добавит комментарий, который будет отображаться в СУБД (добавлять по желанию)
            builder.Property(p => p.GradeNumber)
                .IsRequired()
                .HasColumnName("GradeNumber")
                .HasColumnType(ColumnType.Int)
                .HasComment("Оценка");

            builder.Property(p => p.GradeDate)
                .IsRequired()
                .HasColumnName("GradeDate")
                .HasColumnType(ColumnType.Date)
                .HasComment("Дата получения оценки");

            builder.ToTable(TableName)
                            .HasOne(p => p.Student)
                            .WithMany(t => t.Grades)
                            .HasForeignKey(p => p.StudentId)
                            .HasConstraintName("fk_f_student_id2")
                            .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.StudentId, $"idx_{TableName}fk_f_student_id2");

            //Добавим явную автоподгрузку связанной сущности
            builder.Navigation(p => p.Student)
                .AutoInclude();

            builder.ToTable(TableName)
                            .HasOne(p => p.Subject)
                            .WithMany(t => t.Grades)
                            .HasForeignKey(p => p.SubjectId)
                            .HasConstraintName("fk_f_subject_id2")
                            .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.SubjectId, $"idx_{TableName}fk_f_subject_id2");

            //Добавим явную автоподгрузку связанной сущности
            builder.Navigation(p => p.Subject)
                .AutoInclude();
        }
    }
}
