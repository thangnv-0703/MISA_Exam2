using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Exam.Core.Entities
{
    public class Exercise
    {
        #region Constructor
        public Exercise()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// Id bài tập
        /// </summary>
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ExerciseId { get; set; }
        /// <summary>
        /// Tên bài tập
        /// </summary>
        public string ExerciseName { get; set; }
        /// <summary>
        /// Ảnh bài tập
        /// </summary>
        public string ExerciseImage { get; set; }
        /// <summary>
        /// Id khối lớp
        /// </summary>
        public string GradeId { get; set; }
        /// <summary>
        /// Tên khối lớp
        /// </summary>
        public string GradeName { get; set; }
        /// <summary>
        /// Mã khối lớp
        /// </summary>
        public string GradeCode { get; set; }
        /// <summary>
        /// Id môn học
        /// </summary>
        public string SubjectId { get; set; }        
        /// <summary>
        /// Tên môn học
        /// </summary>
        public string SubjectName { get; set; }        
        /// <summary>
        /// Mã môn học
        /// </summary>
        public string SubjectCode { get; set; }
        /// <summary>
        /// Id chu đề
        /// </summary>
        public object TopicId { get; set; }
        /// <summary>
        /// Tên chủ đề
        /// </summary>
        public object TopicName { get; set; }
        /// <summary>
        /// Danh sách thẻ tìm kiếm
        /// </summary>
        public List<string> SearchTags { get; set; }
        /// <summary>
        /// Trạng thái bài tập
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Danh sách câu hỏi
        /// </summary>
        public List<Question> Questions { get; set; }
        /// <summary>
        /// Số lượng câu hỏi
        /// </summary>
        public int QuestionCount { get; set; }
        
        #endregion

        #region Other
        /// <summary>
        /// Ngày khởi tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người khởi tạo
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        public string ModifiedBy { get; set; }
        #endregion
    }
}
