using System.ComponentModel.DataAnnotations;

namespace Gradebook.DTOs
{
    public record CreateGradeRecordDTO
    {
        [Required]
        [Range(1.0,6.0)]
        public Double Grade { get; init; }
        [Required]
        public string? Description { get; init; }
        [Required]
        public string? Subject { get; init; }
    }
}
