using Gradebook.DTOs;
using Gradebook.Model;
using Gradebook.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Linq;

namespace Gradebook.Controllers
{
    [ApiController]
    [Route("grades")]
    public class GradesController : ControllerBase
    {
        private readonly IGradesRepository _gradesRepository;
        private readonly IStudentsRepository _studentsRepository;
        private readonly ISubjectsRepository _subjectsRepository;
        public GradesController(IGradesRepository gradesRepository, IStudentsRepository studentsRepository, ISubjectsRepository subjectsRepository)
        {
            this._gradesRepository = gradesRepository;
            this._studentsRepository = studentsRepository;
            this._subjectsRepository = subjectsRepository;
        }

        [HttpGet]
        public async Task <IEnumerable<GradeRecordDTO>> GetGradeRecordsAsync()
        {
            var gradeRecords = (await _gradesRepository.GetGradeRecordsAsync()).Select(gradeRecord => gradeRecord.GradeAsDTO());
            return gradeRecords;    
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GradeRecordDTO>> GetGradeRecordAsync(Guid id)
        {
            var gradeRecord = await _gradesRepository.GetGradeRecordAsync(id);
            if (gradeRecord == null)
            {
                return NotFound();
            }
            return Ok(gradeRecord.GradeAsDTO());
        }

        //POST /grades
        [HttpPost]
        public async Task<ActionResult<GradeRecordDTO>> CreateGradeRecordAsync(CreateGradeRecordDTO createGradeRecordDTO)
        {
       
            //Check if student exist
            var existingStudent = await _studentsRepository.GetStudentAsync(createGradeRecordDTO.StudentId);

            if(existingStudent == null) 
            { 
                return NotFound(createGradeRecordDTO.StudentId);
            }

            //Check if subject exist
            var existingSubject = await _subjectsRepository.GetSubjectAsync(createGradeRecordDTO.SubjectId);

            if(existingSubject == null)
            {
                return NotFound(createGradeRecordDTO.SubjectId);
            }

            GradeRecord gradeRecord = new()
            {
                GradeRecordId = Guid.NewGuid(),
                Description = createGradeRecordDTO.Description,
                Grade = createGradeRecordDTO.Grade,
                CreatedDate = DateTimeOffset.UtcNow,
                StudentId = createGradeRecordDTO.StudentId,
                SubjectId = createGradeRecordDTO.SubjectId
            };

            await _gradesRepository.CreateGradeRecordAsync(gradeRecord);

            //Add gradeId to student
            IEnumerable<Guid> UpdatedGradesIds = new List<Guid>();
            UpdatedGradesIds = existingStudent.GradesIds.Append(gradeRecord.GradeRecordId).ToList();
            Student student = existingStudent with
            {
                GradesIds = UpdatedGradesIds.ToList()
            };
            await _studentsRepository.UpdateStudentAsync(student);

            return CreatedAtAction(nameof(GetGradeRecordAsync), new { id = gradeRecord.GradeRecordId}, gradeRecord.GradeAsDTO());
        }

        //PUT /grades/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGradeRecordAsync(Guid id, UpdateGradeRecordDTO updateGradeRecordDTO)
        {
            var existingRecord = await _gradesRepository.GetGradeRecordAsync(id);

            if(existingRecord == null)
            {
                return NotFound();
            }

            //Check if in updatedGradeRecord student and subject exist if they changed
            Student studentPrev = new();
            Student studentNew = new();

            if (existingRecord.StudentId != updateGradeRecordDTO.StudentId)
            {
                var existingStudentNew = await _studentsRepository.GetStudentAsync(updateGradeRecordDTO.StudentId);

                if (existingStudentNew == null)
                {
                    return NotFound(updateGradeRecordDTO.StudentId);
                }
                else
                {
                    var existingStudentPrev = await _studentsRepository.GetStudentAsync(existingRecord.StudentId);
                    
                    //Delete gradeId from previous owner
                    IEnumerable<Guid> UpdatedGradesIdsPrev = existingStudentPrev.GradesIds.Where(i => i != id);
                    studentPrev = existingStudentPrev with
                    {
                        GradesIds = UpdatedGradesIdsPrev
                    };

                    //Adding gradeId to new owner
                    IEnumerable<Guid> UpdatedGradesIdsNew = existingStudentNew.GradesIds.Append(updateGradeRecordDTO.StudentId);
                    studentNew = existingStudentNew with
                    {
                        GradesIds = UpdatedGradesIdsNew
                    };
                }
            }

            if(existingRecord.SubjectId != updateGradeRecordDTO.SubjectId)
            {
                var existingSubject = await _subjectsRepository.GetSubjectAsync(updateGradeRecordDTO.SubjectId);

                if (existingSubject == null)
                {
                    return NotFound(updateGradeRecordDTO.SubjectId);
                }
            }

            GradeRecord updatedGradeRecord = existingRecord with
            {
                Description = updateGradeRecordDTO.Description,
                Grade = updateGradeRecordDTO.Grade,
                StudentId = updateGradeRecordDTO.StudentId,
                SubjectId = updateGradeRecordDTO.SubjectId
            };
            await _gradesRepository.UpdateGradeRecordAsync(updatedGradeRecord);

            //If studentId changed update two student records
            if (existingRecord.StudentId != updateGradeRecordDTO.StudentId)
            {
                await _studentsRepository.UpdateStudentAsync(studentPrev);
                await _studentsRepository.UpdateStudentAsync(studentNew);
            }
            return NoContent();
        }

        //DELETE /grades/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGradeRecordAsync(Guid id) 
        {
            var existingRecord = await _gradesRepository.GetGradeRecordAsync(id);

            if (existingRecord is null)
            {
                return NotFound();
            }

            await _gradesRepository.DeleteGradeRecordAsync(id);

            //TODO 
            //Update student record
            var existingStudent = await _studentsRepository.GetStudentAsync(existingRecord.StudentId);

            //Delete gradeId from previous owner
            IEnumerable<Guid> UpdatedGradesIds = existingStudent.GradesIds.Where(i => i != id);
            Student UpdatedStudent = existingStudent with
            {
                GradesIds = UpdatedGradesIds
            };

            await _studentsRepository.UpdateStudentAsync(UpdatedStudent);

            return NoContent();
        }
    }
}
