namespace Gradebook.DTOs
{
    public class GradeRecordDTO
    {
        public Guid GradeRecordId { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public Double Grade { get; init; }
        public string? Description { get; init; }
        public string? Subject { get; init; }
    }
}
