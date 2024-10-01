using System.Text.Json.Serialization;
namespace PopovaPolinaOZKT_42_21.DataBase.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? SubjectDescription { get; set; }
        [JsonIgnore]
        public List<Exam>? Exams { get; set; }
        [JsonIgnore]
        public List<Grade>? Grades { get; set; }
        public bool IsDeleted { get; set; }
    }
}
