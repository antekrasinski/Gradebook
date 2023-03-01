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

        public void CreateGradeRecord(GradeRecord gradeRecord)
        {
            _gradesCollection.InsertOne(gradeRecord);
        }

        public void DeleteGradeRecord(Guid gradeRecordId)
        {
            var filter = filterBuilder.Eq(existingRecord => existingRecord.GradeRecordId, gradeRecordId);
            _gradesCollection.DeleteOne(filter);
        }

        public GradeRecord GetGradeRecord(Guid gradeRecordId)
        {
            var filter = filterBuilder.Eq(gradeRecord => gradeRecord.GradeRecordId, gradeRecordId);
            return _gradesCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<GradeRecord> GetGradeRecords()
        {
            return _gradesCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateGradeRecord(GradeRecord gradeRecord)
        {
            var filter = filterBuilder.Eq(existingRecord => existingRecord.GradeRecordId, gradeRecord.GradeRecordId);
            _gradesCollection.ReplaceOne(filter, gradeRecord);
        }
    }
}
