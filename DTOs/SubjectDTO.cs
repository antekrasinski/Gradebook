using Gradebook.Model;

namespace Gradebook.DTOs
{
    public class SubjectDTO
    {
        public Guid SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? Teacher { get; set; }
    }
}
