using System.ComponentModel.DataAnnotations;

namespace Gradebook.DTOs
{
    public class UpdateStudentDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? DateOfBirth { get; set; }
        [Required]
        public IEnumerable<Guid> SubjectsIds { get; set; }
        [Required]
        public IEnumerable<Guid> GradesIds { get; set; }
    }
}
