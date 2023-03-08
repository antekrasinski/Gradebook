using Gradebook.Model;

namespace Gradebook.Repositories
{
    public interface ISubjectsRepository
    {
        Task<Subject> GetSubjectAsync(Guid subjectId);
        Task<IEnumerable<Subject>> GetSubjects();
        Task CreateSubjectAsync(Subject subject);
        Task UpdateSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(Guid subjectId);
    }
}
