using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Exam.Core.Entities
{
    public class Topic
    {
        #region Constructor
        public Topic()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// Id chủ đề
        /// </summary>
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string TopicId { get; set; }
        /// <summary>
        /// Tên chủ đề
        /// </summary>
        public string TopicName { get; set; }
        /// <summary>
        /// Id môn học
        /// </summary>
        public string SubjectId { get; set; }
        /// <summary>
        /// Mã môn học
        /// </summary>
        public string SubjectCode { get; set; }
 /*       /// <summary>
        /// Id khối lớp
        /// </summary>
        public string GradeId { get; set; }
        /// <summary>
        /// Mã khối lớp
        /// </summary>
        public string GradeCode { get; set; }*/

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
