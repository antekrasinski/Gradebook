using Gradebook.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data.Common;

namespace Gradebook.Repositories
{
    public class MongoDbGradesRepository : IGradesRepository
    {
        private const string dbName = "gradebook";

        private const string collectionName = "grades";

        private readonly IMongoCollection<GradeRecord> _gradesCollection;

        private readonly FilterDefinitionBuilder<GradeRecord> filterBuilder = Builders<GradeRecord>.Filter;

        public MongoDbGradesRepository(IMongoClient mongoClient) 
        {
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            _gradesCollection = database.GetCollection<GradeRecord>(collectionName);
        }

        public  async Task CreateGradeRecordAsync(GradeRecord gradeRecord)
        {
            await _gradesCollection.InsertOneAsync(gradeRecord);
        }

        public async Task DeleteGradeRecordAsync(Guid gradeRecordId)
        {
            var filter = filterBuilder.Eq(existingRecord => existingRecord.GradeRecordId, gradeRecordId);
            await _gradesCollection.DeleteOneAsync(filter);
        }

        public async Task<GradeRecord> GetGradeRecordAsync(Guid gradeRecordId)
        {
            var filter = filterBuilder.Eq(gradeRecord => gradeRecord.GradeRecordId, gradeRecordId);
            return await _gradesCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<GradeRecord>> GetGradeRecords()
        {
            return await _gradesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateGradeRecordAsync(GradeRecord gradeRecord)
        {
            var filter = filterBuilder.Eq(existingRecord => existingRecord.GradeRecordId, gradeRecord.GradeRecordId);
            await _gradesCollection.ReplaceOneAsync(filter, gradeRecord);
        }
    }
}
