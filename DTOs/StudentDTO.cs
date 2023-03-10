using Gradebook.Model;

namespace Gradebook.DTOs
{
    public class StudentDTO
    {
        public Guid StudentId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public IEnumerable<Guid>? SubjectsIds { get; set; }
        public IEnumerable<Guid>? GradesIds { get; set; }

    }
}
