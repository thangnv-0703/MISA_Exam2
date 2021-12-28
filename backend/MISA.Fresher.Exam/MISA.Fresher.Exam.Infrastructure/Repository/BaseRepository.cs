using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Fresher.Exam.Core.Interfaces.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Exam.Infrastructure.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region Field
        /// <summary>
        /// Chuỗi kết nối
        /// </summary>
        protected string _connectionString;
        /// <summary>
        /// Tên bảng
        /// </summary>
        protected string _tableName;
        /// <summary>
        /// Mongo Client
        /// </summary>
        protected MongoClient _client;
        /// <summary>
        /// Mongo database
        /// </summary>
        protected IMongoDatabase _database;
        #endregion

        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            // Khai báo thông tin kết nối với database
            _connectionString = configuration.GetConnectionString("ExamConnection");
            // Tạo tên bảng
            _tableName = typeof(TEntity).Name;
            // Khởi tạo kết nối
            _client = new MongoClient(_connectionString);
            // Thực hiện kết nối với database
            _database = _client.GetDatabase("Test2_Education");
        }
        #endregion

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="entityId">Id thực thể</param>
        /// <returns>số lượng nhân xóa thành công</returns>
        /// Created by: NVTHANG(18/11/2021)
        public virtual int Delete(string entityId)
        {
            var collection = _database.GetCollection<TEntity>($"{_tableName}");
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(entityId));
            var result = collection.DeleteOne(filter);
            return (int)result.DeletedCount;
        }

        /// <summary>
        /// Lấy toàn bộ dữ liẹu vật thể
        /// </summary>
        /// <returns>List đối tượng</returns>
        /// Created by: NVTHANG(18/11/2021)
        public virtual List<TEntity> GetAll()
        {
            var collection = _database.GetCollection<TEntity>($"{_tableName}");
            var documents = collection.Find(new BsonDocument());
            return documents.ToList();
        }

        /// <summary>
        /// Lấy dữ liệu theo Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Đối tượng</returns>
        /// Created by: NVTHANG(18/11/2021)
        public virtual TEntity GetById(string entityId)
        {
            var collection = _database.GetCollection<TEntity>($"{_tableName}");
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(entityId));
            var documents = collection.Find(filter).FirstOrDefault();
            return documents;
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Thông tin thực thể</param>
        /// <returns>số lượng nhân viên thêm mới thành công</returns>
        /// Created by: NVTHANG(18/11/2021)
        public virtual string Insert(TEntity entity)
        {
            var collection = _database.GetCollection<TEntity>($"{_tableName}");
            collection.InsertOne(entity);
            var id = "";
            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                var propValue = prop.GetValue(entity);
                var propName = prop.Name;
                if (propName == $"{_tableName}Id")
                {
                    id = propValue.ToString();
                    break;
                }
            }
            return id;
        }

        /// <summary>
        /// Sửa
        /// </summary>
        /// <param name="entityId">Id thực thể</param>
        /// <param name="entity">Thông tin thực thể</param>
        /// <returns>số lượng nhân viên sửa thành công</returns>
        /// Created by: NVTHANG(18/11/2021)
        public virtual int Update(TEntity entity, string entityId)
        {
            var collection = _database.GetCollection<TEntity>($"{_tableName}");
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(entityId));
            var result = collection.ReplaceOne(filter, entity);
            return (int)result.MatchedCount;
        }
    }
}
