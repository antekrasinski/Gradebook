using Gradebook.Model;

namespace Gradebook.Repositories
{
    public interface IGradesRepository
    {
        Task<GradeRecord> GetGradeRecordAsync(Guid gradeRecordId);
        Task<IEnumerable<GradeRecord>> GetGradeRecords();
        Task CreateGradeRecordAsync (GradeRecord gradeRecord);
        Task UpdateGradeRecordAsync(GradeRecord gradeRecord);
        Task DeleteGradeRecordAsync(Guid gradeRecordId);
    }

}
