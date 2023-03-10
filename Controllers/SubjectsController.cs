using Gradebook.DTOs;
using Gradebook.Model;
using Gradebook.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.Controllers
{
    [ApiController]
    [Route("subjects")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectsRepository _subjectsRepository;
        public SubjectsController(ISubjectsRepository subjectsRepository)
        {
            this._subjectsRepository = subjectsRepository;
        }

        //GET /subjects
        [HttpGet]
        public async Task<IEnumerable<SubjectDTO>> GetSubjects()
        {
            var subjects = (await _subjectsRepository.GetSubjectsAsync()).Select(subject => subject.SubjectAsDTO());
            return subjects;
        }

        //GET /subjects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDTO>> GetSubjectAsync(Guid id)
        {
            var subject = await _subjectsRepository.GetSubjectAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject.SubjectAsDTO());
        }

        //POST /subjects
        [HttpPost]
        public async Task<ActionResult<SubjectDTO>> CreateSubjectAsync(CreateSubjectDTO dto)
        {
            Subject subject = new()
            {
                SubjectId = Guid.NewGuid(),
                SubjectName = dto.SubjectName,
                Teacher = dto.Teacher
            };

            await _subjectsRepository.CreateSubjectAsync(subject);
            return CreatedAtAction(nameof(GetSubjectAsync), new { id = subject.SubjectId }, subject.SubjectAsDTO());
        }

        //PUT /subjects/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSubjectAsync (Guid id, UpdateSubjectDTO dto)
        {
            var existingSubject = await _subjectsRepository.GetSubjectAsync(id);

            if(existingSubject == null)
            {
                return NotFound();
            }

            Subject updatedSubject = existingSubject with
            {
                SubjectName = dto.SubjectName,
                Teacher = dto.Teacher
            };

            await _subjectsRepository.UpdateSubjectAsync(updatedSubject);
            return NoContent();
        }

        //DELETE /subjects/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubjectAsync (Guid id)
        {
            var existingSubject = await _subjectsRepository.GetSubjectAsync(id);
            
            if(existingSubject == null)
            {
                return NotFound();
            }
            await _subjectsRepository.DeleteSubjectAsync(id);

            return NoContent();
        }
    }
}
