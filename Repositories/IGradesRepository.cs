using Gradebook.Model;

namespace Gradebook.Repositories
{
    public interface IGradesRepository
    {
        Task<GradeRecord> GetGradeRecordAsync(Guid gradeRecordId);
        Task<IEnumerable<GradeRecord>> GetGradeRecordsAsync();
        Task CreateGradeRecordAsync (GradeRecord gradeRecord);
        Task UpdateGradeRecordAsync(GradeRecord gradeRecord);
        Task DeleteGradeRecordAsync(Guid gradeRecordId);
        Task DeleteAllStudentsGradesAsync(Guid studentId);
    }

}
