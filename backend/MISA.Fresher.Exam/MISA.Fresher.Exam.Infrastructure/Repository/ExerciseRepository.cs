using Fare;
using Microsoft.Extensions.Configuration;
using MISA.Fresher.Exam.Core.Entities;
using MISA.Fresher.Exam.Core.Interfaces.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Exam.Infrastructure.Repository
{
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        private IMongoCollection<Exercise> _collection;
        public ExerciseRepository(IConfiguration configuration) : base(configuration)
        {
            _collection = _database.GetCollection<Exercise>("Exercise");
        }

        public override List<Exercise> GetAll()
        {
            var collection = _database.GetCollection<Exercise>("Exercise");
            var sort = Builders<Exercise>.Sort.Descending("ExerciseId");
            var documents = collection.Find(new BsonDocument()).Sort(sort);
            return documents.ToList();
        }

        /// <summary>
        /// Lọc bài tập theo tên, môn học, khối lớp, chủ đề, trạng thái và thẻ tìm kiếm 
        /// với thông tin về số bản ghi/1 trang và số thứ tự trang
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="subjectId"></param>
        /// <param name="gradeId"></param>
        /// <param name="topicId"></param>
        /// <param name="status"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns>Danh sách nhân viên sau khi search, tổng số bản ghi, tổng số trang</returns>
        public object GetExerciseePaging(string searchText, string subjectId, string gradeId, string topicId, int status, int pageSize, int pageIndex)
        {
            var filter = Builders<Exercise>.Filter.Empty;
            if (!string.IsNullOrEmpty(searchText))
            {
                filter &= Builders<Exercise>.Filter.Where(x => x.ExerciseName.ToLower().Contains(searchText.ToLower())  );
            }

            if (!String.IsNullOrEmpty(subjectId) && subjectId != "-1")
            {
                filter &= Builders<Exercise>.Filter.Eq(x => x.SubjectId, subjectId);
            }
            if (!String.IsNullOrEmpty(gradeId) && gradeId != "-1")
            {
                filter &= Builders<Exercise>.Filter.Eq(x => x.GradeId, gradeId);
            }
            if (!String.IsNullOrEmpty(topicId) && topicId != "-1")
            {
                filter &= Builders<Exercise>.Filter.Eq(x => x.TopicId, topicId);
            }
            if (status == 0 || status == 1)
            {
                filter &= Builders<Exercise>.Filter.Eq(x => x.Status, status);
            }
            var project = Builders<Exercise>.Projection.Include(x => x.ExerciseId)
                                                        .Include(x => x.ExerciseName)
                                                        .Include(x => x.SubjectName)
                                                        .Include(x => x.GradeName)
                                                        .Include(x => x.Status)
                                                        .Include(x => x.ExerciseImage)
                                                        .Include(x => x.QuestionCount);
            //var collection = _database.GetCollection<Exercise>("Exercise");
            var sort = Builders<Exercise>.Sort.Descending("ExerciseId");
            var documents = _collection.Find(filter).Project<Exercise>(project).Sort(sort).Limit(pageSize).Skip(pageSize * (pageIndex - 1)).ToList();
            return new 
            {
                exercise= documents,
                totalRecord = documents.Count,
            };
        }
    }
}
