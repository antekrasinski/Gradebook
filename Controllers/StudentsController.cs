using Gradebook.DTOs;
using Gradebook.Model;
using Gradebook.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IGradesRepository _gradesRepository;
        public StudentsController(IStudentsRepository studentsRepository, IGradesRepository gradesRepository) 
        {
            this._studentsRepository = studentsRepository;  
            this._gradesRepository = gradesRepository;
        }

        //GET /students
        [HttpGet]
        public async Task<IEnumerable<StudentDTO>> GetStudentsAsync()
        {
            var students = (await _studentsRepository.GetStudentsAsync()).Select(student => student.StudentAsDTO());
            return students;
        }

        //GET /students/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudentAsync(Guid id)
        {
            var student = await _studentsRepository.GetStudentAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            return Ok(student.StudentAsDTO());
        }

        //POST /students
        [HttpPost]
        public async Task<ActionResult<StudentDTO>> CreateStudentAsync(CreateStudentDTO dto)
        {
            //TODO
            //Parsing exeptions hadling
            DateTimeOffset.TryParse(dto.DateOfBirth, out var dateOfBirth);
            Student student = new()
            {
                StudentId = Guid.NewGuid(),
                Name = dto.Name,
                Surname = dto.Surname,
                DateOfBirth = dateOfBirth,
                GradesIds = new List<Guid>(),
                SubjectsIds = new List<Guid>()
            };
            await _studentsRepository.CreateStudentAsync(student); 
            return CreatedAtAction(nameof(GetStudentAsync), new {id = student.StudentId}, student.StudentAsDTO());
        }

        //PUT /students/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudentAsync(Guid id, UpdateStudentDTO dto)
        {
            var existingStudent = await _studentsRepository.GetStudentAsync(id);
        
            if(existingStudent == null) 
            {
                return NotFound();
            }

            //TODO
            //Parsing exeptions hadling
            DateTimeOffset.TryParse(dto.DateOfBirth, out var dateOfBirth);

            Student updatedStudent = existingStudent with
            {
                Name = dto.Name,
                Surname = dto.Surname,
                DateOfBirth = dateOfBirth,
                GradesIds = dto.GradesIds,
                SubjectsIds = dto.SubjectsIds
            };

            return NoContent();
        }

        //DELETE /students/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudentAsync(Guid id)
        {
            var existingStudent = await _studentsRepository.GetStudentAsync(id);

            if(existingStudent == null) 
            {
                return NotFound();
            }

            await _studentsRepository.DeleteStudentAsync(id);
            //Delete all grades that belong to student
            await _gradesRepository.DeleteAllStudentsGradesAsync(id);

            return NoContent();
        }
    }
}
