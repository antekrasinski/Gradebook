using System.ComponentModel.DataAnnotations;

namespace Gradebook.DTOs
{
    public class UpdateSubjectDTO
    {
        [Required]
        public string? SubjectName { get; set; }
        [Required]
        public string? Teacher { get; set; }
    }
}
