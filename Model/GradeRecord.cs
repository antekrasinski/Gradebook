namespace Gradebook.Model
{
    public record GradeRecord
    {
        public Guid GradeRecordId { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public Double Grade { get; init; }
        public string? Description { get; init; } 
        public string? Subject { get; init; }
    }
}
