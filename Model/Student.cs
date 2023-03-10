using MongoDB.Bson.Serialization.Attributes;

namespace Gradebook.Model
{
    [BsonIgnoreExtraElements]
    public record Student
    {
        public Guid StudentId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public IEnumerable<Guid>? SubjectsIds { get; set; }
        public IEnumerable<Guid>? GradesIds { get; set; }
    }
}
