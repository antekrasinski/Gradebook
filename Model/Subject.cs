using MongoDB.Bson.Serialization.Attributes;

namespace Gradebook.Model
{
    [BsonIgnoreExtraElements]
    public record Subject
    {
        public Guid SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? Teacher { get; set; }
    }
}
