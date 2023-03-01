using Gradebook.DTOs;
using Gradebook.Model;

namespace Gradebook
{
    public static class Extensions
    {
        public static GradeRecordDTO AsDTO(this GradeRecord gradeRecord) 
        {
            return new GradeRecordDTO
            {
                GradeRecordId = gradeRecord.GradeRecordId,
                CreatedDate = gradeRecord.CreatedDate,
                Grade = gradeRecord.Grade,
                Description = gradeRecord.Description,
                Subject = gradeRecord.Subject
            };
        }
    }
}
