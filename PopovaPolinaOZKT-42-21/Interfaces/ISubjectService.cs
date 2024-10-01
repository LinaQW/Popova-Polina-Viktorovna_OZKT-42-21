using PopovaPolinaOZKT_42_21.DataBase.Models;
using PopovaPolinaOZKT_42_21.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PopovaPolinaOZKT_42_21.Filters.SubjectFilters;
using PopovaPolinaOZKT_42_21.Filters.SubjectsFilters;
//using PopovaPolinaOZKT_42_21.Filters.GroupFilters;

namespace PopovaPolinaOZKT_42_21.Interfaces
{
    public interface ISubjectService
    {
        public Task<Subject[]> GetSubjectsByDescriptionAsync(SubjectDescriptionFilter filter,CancellationToken cancellationToken);
        public Task<Subject[]> GetSubjectsByIsDeletedAsync(SubjectIsDeletedFilter filter, CancellationToken cancellationToken);
    }

    public class SubjectService : ISubjectService
    {
        private readonly StudentDbContext _dbContext;
        public SubjectService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Subject[]> GetSubjectsByDescriptionAsync(SubjectDescriptionFilter filter, CancellationToken cancellationToken = default)
        {
            var subjects = _dbContext.Set<Subject>().Where(w => w.SubjectDescription == filter.SubjectDescription).Where(w => w.IsDeleted == filter.SubjectIsDeleted).ToArrayAsync(cancellationToken);
            return subjects;
        }
        public Task<Subject[]> GetSubjectsByIsDeletedAsync(SubjectIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var subjects = _dbContext.Set<Subject>().Where(w => w.IsDeleted == filter.SubjectIsDeleted).ToArrayAsync(cancellationToken);
            return subjects;
        }
    }

}
