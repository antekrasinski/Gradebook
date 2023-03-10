using System.ComponentModel.DataAnnotations;

namespace Gradebook.DTOs
{
    public class CreateStudentDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? DateOfBirth { get; set; }
    }
}
