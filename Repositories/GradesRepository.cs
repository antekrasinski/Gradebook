using Gradebook.Model;

namespace Gradebook.Repositories
{
    public class GradesRepository : IGradesRepository
    {
        private readonly List<GradeRecord> gradeRecords = new()
        {
            new GradeRecord {GradeRecordId = Guid.NewGuid(), CreatedDate = DateTime.Now, Description = "Exam", Grade = 4.5, Subject = "Math" },
            new GradeRecord {GradeRecordId = Guid.NewGuid(), CreatedDate = DateTime.Now, Description = "Homework", Grade = 4, Subject = "Math" },
            new GradeRecord {GradeRecordId = Guid.NewGuid(), CreatedDate = DateTime.Now, Description = "Group project", Grade = 5, Subject = "Math" }
        };

        public IEnumerable<GradeRecord> GetGradeRecords()
        {
            return gradeRecords;
        }

        public GradeRecord GetGradeRecord(Guid gradeRecordId)
        {
            return gradeRecords.SingleOrDefault(gradeRecord => gradeRecord.GradeRecordId == gradeRecordId);
        }

        public void CreateGradeRecord(GradeRecord gradeRecord)
        {
            gradeRecords.Add(gradeRecord);
        }

        public void UpdateGradeRecord(GradeRecord gradeRecord)
        {
            var index = gradeRecords.FindIndex(existingRecord => existingRecord.GradeRecordId == gradeRecord.GradeRecordId);
            gradeRecords[index] = gradeRecord;
        }

        public void DeleteGradeRecord(Guid gradeRecordId)
        {
            var index = gradeRecords.FindIndex(existingRecord => existingRecord.GradeRecordId == gradeRecordId);
            gradeRecords.RemoveAt(index);
        }
    }
}
