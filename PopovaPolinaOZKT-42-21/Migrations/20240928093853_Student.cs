﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PopovaPolinaOZKT_42_21.Migrations
{
    /// <inheritdoc />
    public partial class Student : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи группы")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupName = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название группы"),
                    GroupJob = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Специальность группы"),
                    GroupYear = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Год поступления"),
                    StudentQuantity = table.Column<int>(type: "int4", nullable: false, comment: "Количество студентов в группе"),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false, comment: "Статус удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Groups_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи дисциплины")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectName = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название дисциплины"),
                    SubjectDescription = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Направление дисциплины"),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false, comment: "Статус удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Subject_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи студента")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Surname = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Имя студента"),
                    Name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Фамилия студента"),
                    Midname = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Отчество студента"),
                    GroupId = table.Column<int>(type: "int4", nullable: false, comment: "Идентификатор группы"),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false, comment: "Статус удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Students_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_f_group_id",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи зачета")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExamName = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название зачета"),
                    DateOfTest = table.Column<DateTime>(type: "timestamp", nullable: false, comment: "Дата проведения зачета"),
                    ExamCondition = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Состояние зачета (сдан/не сдан)"),
                    SubjectId = table.Column<int>(type: "integer", nullable: true),
                    StudentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Exam_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_f_student_id1",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_f_subject_id1",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи зачета")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GradeNumber = table.Column<int>(type: "int4", nullable: false, comment: "Оценка"),
                    GradeDate = table.Column<DateTime>(type: "timestamp", nullable: false, comment: "Дата получения оценки"),
                    SubjectId = table.Column<int>(type: "integer", nullable: true),
                    StudentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Grade_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_f_student_id2",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_f_subject_id2",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_Examfk_f_student_id1",
                table: "Exam",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "idx_Examfk_f_subject_id1",
                table: "Exam",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "idx_Gradefk_f_student_id2",
                table: "Grade",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "idx_Gradefk_f_subject_id2",
                table: "Grade",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "idx_Students_fk_f_group_id",
                table: "Students",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
