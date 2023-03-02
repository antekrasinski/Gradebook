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
        public async Task <IEnumerable<GradeRecordDTO>> GetGradeRecordsAsync()
        {
            var gradeRecords = (await _gradesRepository.GetGradeRecords()).Select(gradeRecord => gradeRecord.AsDTO());
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
            return Ok(gradeRecord.AsDTO());
        }

        //POST /grades
        [HttpPost]
        public async Task<ActionResult<GradeRecordDTO>> CreateGradeRecordAsync(CreateGradeRecordDTO createGradeRecordDTO)
        {
            GradeRecord gradeRecord = new()
            {
                GradeRecordId = Guid.NewGuid(),
                Description = createGradeRecordDTO.Description,
                Subject = createGradeRecordDTO.Subject,
                Grade = createGradeRecordDTO.Grade,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _gradesRepository.CreateGradeRecordAsync(gradeRecord);
            return CreatedAtAction(nameof(GetGradeRecordAsync), new { id = gradeRecord.GradeRecordId}, gradeRecord.AsDTO());
        }

        //PUT /grades/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGradeRecordAsync(Guid id, UpdateGradeRecordDTO updateGradeRecordDTO)
        {
            var existingRecord = await _gradesRepository.GetGradeRecordAsync(id);

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

            await _gradesRepository.UpdateGradeRecordAsync(updatedGradeRecord);

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

            return NoContent();
        }
    }
}
