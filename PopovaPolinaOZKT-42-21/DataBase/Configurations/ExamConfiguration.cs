using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PopovaPolinaOZKT_42_21.DataBase.Helpers;
using PopovaPolinaOZKT_42_21.DataBase.Models;


namespace PopovaPolinaOZKT_42_21.DataBase.Configurations
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        private const string TableName = "Exam";
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            //Задаем первичный ключ
            builder
                .HasKey(p => p.ExamId)
                .HasName($"pk_{TableName}_Id");

            //Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
            builder.Property(p => p.ExamId)
                    .ValueGeneratedOnAdd();

            //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.ExamId)
                .HasColumnName("Id")
                .HasComment("Идентификатор записи зачета");

            //HasComment добавит комментарий, который будет отображаться в СУБД (добавлять по желанию)
            builder.Property(p => p.ExamName)
                .IsRequired()
                .HasColumnName("ExamName")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Название зачета");

            builder.Property(p => p.DateOfTest)
                .IsRequired()
                .HasColumnName("DateOfTest")
                .HasColumnType(ColumnType.Date)
                .HasComment("Дата проведения зачета");

            builder.Property(p => p.ExamCondition)
                .IsRequired()
                .HasColumnName("ExamCondition")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Состояние зачета (сдан/не сдан)");

            builder.ToTable(TableName)
                            .HasOne(p => p.Student)
                            .WithMany(t => t.Exams)
                            .HasForeignKey(p => p.StudentId)
                            .HasConstraintName("fk_f_student_id1")
                            .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.StudentId, $"idx_{TableName}fk_f_student_id1");

            //Добавим явную автоподгрузку связанной сущности
            builder.Navigation(p => p.Student)
                .AutoInclude();

            builder.ToTable(TableName)
                            .HasOne(p => p.Subject)
                            .WithMany(t => t.Exams)
                            .HasForeignKey(p => p.SubjectId)
                            .HasConstraintName("fk_f_subject_id1")
                            .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.SubjectId, $"idx_{TableName}fk_f_subject_id1");

            //Добавим явную автоподгрузку связанной сущности
            builder.Navigation(p => p.Subject)
                .AutoInclude();
        }
    }
}
