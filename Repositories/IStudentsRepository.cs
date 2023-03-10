using Gradebook.Model;

namespace Gradebook.Repositories
{
    public interface IStudentsRepository
    {
        Task<Student> GetStudentAsync(Guid studentId);
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task CreateStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Guid studentId);

    }
}
