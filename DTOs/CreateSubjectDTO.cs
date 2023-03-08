using System.ComponentModel.DataAnnotations;

namespace Gradebook.DTOs
{
    public class CreateSubjectDTO
    {
        [Required]
        public string? SubjectName { get; set; }
        [Required]
        public string? Teacher { get; set; }
    }
}
