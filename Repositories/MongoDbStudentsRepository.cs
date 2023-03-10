using Gradebook.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Gradebook.Repositories
{
    public class MongoDbStudentsRepository : IStudentsRepository
    {
        private const string dbName = "gradebook";

        private const string studentsCollectionName = "students";

        private readonly IMongoCollection<Student> _studentsCollection;

        private readonly FilterDefinitionBuilder<Student> filterBuilder = Builders<Student>.Filter;

        public MongoDbStudentsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            _studentsCollection = database.GetCollection<Student>(studentsCollectionName);
        }

        public async Task CreateStudentAsync(Student student)
        {
            await _studentsCollection.InsertOneAsync(student);
        }

        public async Task DeleteStudentAsync(Guid studentId)
        {
            var filter = filterBuilder.Eq(existingStudent => existingStudent.StudentId, studentId);
            await _studentsCollection.DeleteOneAsync(filter);
        }

        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            var filter = filterBuilder.Eq(existingStudent => existingStudent.StudentId, studentId);
            return await _studentsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _studentsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            var filter = filterBuilder.Eq(existingStudent => existingStudent.StudentId, student.StudentId);
            await _studentsCollection.ReplaceOneAsync(filter, student);
        }
    }
}
