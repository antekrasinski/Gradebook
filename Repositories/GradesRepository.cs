using Gradebook.Model;

namespace Gradebook.Repositories
{
    public class GradesRepository
    {
        private readonly List<GradeRecord> gradeRecords = new()
        {
            new GradeRecord {GradeRecordId = Guid.NewGuid(), CreatedDate = DateTime.Now, Description = "Exam", Grade = 4.5, Subject = "Math" },
            new GradeRecord {GradeRecordId = Guid.NewGuid(), CreatedDate = DateTime.Now, Description = "Homework", Grade = 4, Subject = "Math" },
            new GradeRecord {GradeRecordId = Guid.NewGuid(), CreatedDate = DateTime.Now, Description = "Group project", Grade = 5, Subject = "Math" }
        };

        public IEnumerable<GradeRecord> GetItems()
        {
            return gradeRecords;
        }

        public GradeRecord GetGradeRecord(Guid gradeRecordId)
        {
            return gradeRecords.SingleOrDefault(gradeRecord => gradeRecord.GradeRecordId == gradeRecordId);
        }
    }
}
