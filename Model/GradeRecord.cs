using MongoDB.Bson.Serialization.Attributes;

namespace Gradebook.Model
{
    [BsonIgnoreExtraElements]
    public record GradeRecord
    {
        public Guid GradeRecordId { get; init; }
        public Double Grade { get; init; }
        public string? Description { get; init; } 
        public DateTimeOffset CreatedDate { get; init; }
        public Guid StudentId { get; init; }
        public Guid SubjectId { get; init; }
    }
}
