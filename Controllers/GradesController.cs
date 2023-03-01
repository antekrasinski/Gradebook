using Gradebook.DTOs;
using Gradebook.Model;
using Gradebook.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.Controllers
{
    [ApiController]
    [Route("grades")]
    public class GradesController : ControllerBase
    {
        private readonly IGradesRepository _gradesRepository;

        public GradesController(IGradesRepository repository)
        {
            this._gradesRepository = repository;
        }

        [HttpGet]
        public IEnumerable<GradeRecordDTO> GetGradeRecords()
        {
            var gradeRecords = _gradesRepository.GetGradeRecords().Select(gradeRecord => gradeRecord.AsDTO());
            return gradeRecords;    
        }

        [HttpGet("{id}")]
        public ActionResult<GradeRecordDTO> GetGradeRecord(Guid id)
        {
            var gradeRecord = _gradesRepository.GetGradeRecord(id);
            if (gradeRecord == null)
            {
                return NotFound();
            }
            return Ok(gradeRecord.AsDTO());
        }

        //POST /grades
        [HttpPost]
        public ActionResult<GradeRecordDTO> CreateGradeRecord(CreateGradeRecordDTO createGradeRecordDTO)
        {
            GradeRecord gradeRecord = new()
            {
                GradeRecordId = Guid.NewGuid(),
                Description = createGradeRecordDTO.Description,
                Subject = createGradeRecordDTO.Subject,
                Grade = createGradeRecordDTO.Grade,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _gradesRepository.CreateGradeRecord(gradeRecord);
            return CreatedAtAction(nameof(GetGradeRecord), new { id = gradeRecord.GradeRecordId}, gradeRecord.AsDTO());
        }

        //PUT /grades/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateGradeRecord(Guid id, UpdateGradeRecordDTO updateGradeRecordDTO)
        {
            var existingRecord = _gradesRepository.GetGradeRecord(id);

            if(existingRecord is null)
            {
                return NotFound();
            }

            GradeRecord updatedGradeRecord = existingRecord with
            {
                Description = updateGradeRecordDTO.Description,
                Subject = updateGradeRecordDTO.Subject,
                Grade = updateGradeRecordDTO.Grade
            };

            _gradesRepository.UpdateGradeRecord(updatedGradeRecord);

            return NoContent();
        }

        //DELETE /grades/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteGradeRecord(Guid id) 
        {
            var existingRecord = _gradesRepository.GetGradeRecord(id);

            if (existingRecord is null)
            {
                return NotFound();
            }

            _gradesRepository.DeleteGradeRecord(id);

            return NoContent();
        }
    }
}
