using PopovaPolinaOZKT_42_21.DataBase.Models;
using PopovaPolinaOZKT_42_21.DataBase;
using PopovaPolinaOZKT_42_21.Filters.StudentFilters;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace PopovaPolinaOZKT_42_21.Interfaces.StudentsInterfaces
{
    public interface IStudentService
    {
        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
        public Task<Student[]> GetStudentsByFIOAsync(StudentFIOFilter filter, CancellationToken cancellationToken);
        public Task<Student[]> GetStudentsByIsDeletedAsync(StudentIsDeletedFilter filter, CancellationToken cancellationToken);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _dbContext;
        public StudentService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Student[]> GetStudentsByGroupAsync
            (StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext
                .Set<Student>()
                .Where(w => w.Group.GroupName == filter.GroupName)
                .Where(w => w.IsDeleted == filter.StudentIsDeleted)
                .ToArrayAsync(cancellationToken);
            return students;
        }
        public Task<Student[]> GetStudentsByFIOAsync
            (StudentFIOFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>()
                .Where(w => (w.Surname == filter.FIO) || (w.Name == filter.FIO) || (w.Midname == filter.FIO))
                .Where(w => w.IsDeleted == filter.StudentIsDeleted).ToArrayAsync(cancellationToken); 
            
            return students;
        }
        public Task<Student[]> GetStudentsByIsDeletedAsync
            (StudentIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>().Where(w => w.IsDeleted == filter.StudentIsDeleted).ToArrayAsync(cancellationToken);
            return students;
        }
    }
}
