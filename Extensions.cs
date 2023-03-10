using Gradebook.DTOs;
using Gradebook.Model;

namespace Gradebook
{
    public static class Extensions
    {
        public static GradeRecordDTO GradeAsDTO(this GradeRecord gradeRecord) 
        {
            return new GradeRecordDTO
            {
                GradeRecordId = gradeRecord.GradeRecordId,
                CreatedDate = gradeRecord.CreatedDate,
                Grade = gradeRecord.Grade,
                Description = gradeRecord.Description,
                StudentId = gradeRecord.StudentId,
                SubjectId = gradeRecord.SubjectId
            };
        }
        public static SubjectDTO SubjectAsDTO(this Subject subject)
        {
            return new SubjectDTO
            {
                SubjectId = subject.SubjectId,
                SubjectName = subject.SubjectName,
                Teacher = subject.Teacher
            };
        }
        public static StudentDTO StudentAsDTO(this Student student)
        {
            return new StudentDTO
            {
                StudentId = student.StudentId,
                Name = student.Name,
                Surname = student.Surname,
                DateOfBirth = student.DateOfBirth,
                SubjectsIds = student.SubjectsIds,
                GradesIds = student.GradesIds
            };
        }
    }
}
