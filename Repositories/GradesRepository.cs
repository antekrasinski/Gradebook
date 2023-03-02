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

        public async Task<IEnumerable<GradeRecord>> GetGradeRecords()
        {
            return await Task.FromResult(gradeRecords);
        }

        public async Task<GradeRecord> GetGradeRecordAsync(Guid gradeRecordId)
        {
            return await Task.FromResult(gradeRecords.SingleOrDefault(gradeRecord => gradeRecord.GradeRecordId == gradeRecordId));
        }

        public async Task CreateGradeRecordAsync(GradeRecord gradeRecord)
        {
            gradeRecords.Add(gradeRecord);
            await Task.CompletedTask;
        }

        public async Task UpdateGradeRecordAsync(GradeRecord gradeRecord)
        {
            var index = gradeRecords.FindIndex(existingRecord => existingRecord.GradeRecordId == gradeRecord.GradeRecordId);
            gradeRecords[index] = gradeRecord;
            await Task.CompletedTask;
        }

        public async Task DeleteGradeRecordAsync(Guid gradeRecordId)
        {
            var index = gradeRecords.FindIndex(existingRecord => existingRecord.GradeRecordId == gradeRecordId);
            gradeRecords.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}
