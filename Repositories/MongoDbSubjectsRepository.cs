using Gradebook.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;

namespace Gradebook.Repositories
{
    public class MongoDbSubjectsRepository : ISubjectsRepository
    {
        private const string dbName = "gradebook";

        private const string subjectsCollectionName = "subjects";

        private readonly IMongoCollection<Subject> _subjectsCollection;

        private readonly FilterDefinitionBuilder<Subject> filterBuilder = Builders<Subject>.Filter;

        public MongoDbSubjectsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            _subjectsCollection = database.GetCollection<Subject>(subjectsCollectionName);
        }

        public async Task CreateSubjectAsync(Subject subject)
        {
            await _subjectsCollection.InsertOneAsync(subject);
        }

        public async Task DeleteSubjectAsync(Guid subjectId)
        {
            var filter = filterBuilder.Eq(existingSubject => existingSubject.SubjectId, subjectId);
            await _subjectsCollection.DeleteOneAsync(filter);
        }

        public async Task<Subject> GetSubjectAsync(Guid subjectId)
        {
            var filter = filterBuilder.Eq(existingSubject => existingSubject.SubjectId, subjectId);
            return await _subjectsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjects()
        {
            return await _subjectsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateSubjectAsync(Subject subject)
        {
            var filter = filterBuilder.Eq(existingSubject => existingSubject.SubjectId, subject.SubjectId);
            await _subjectsCollection.ReplaceOneAsync(filter, subject);
        }
    }
}
