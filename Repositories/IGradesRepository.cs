using Gradebook.Model;

namespace Gradebook.Repositories
{
    public interface IGradesRepository
    {
        GradeRecord GetGradeRecord(Guid gradeRecordId);
        IEnumerable<GradeRecord> GetGradeRecords();
        void CreateGradeRecord (GradeRecord gradeRecord);
        void UpdateGradeRecord(GradeRecord gradeRecord);
        void DeleteGradeRecord(Guid gradeRecordId);
    }

}
