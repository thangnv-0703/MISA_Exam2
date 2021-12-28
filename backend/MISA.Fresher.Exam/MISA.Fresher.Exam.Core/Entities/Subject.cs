using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Exam.Core.Entities
{
    public class Subject
    {
        #region Constructor
        public Subject()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// Id môn học
        /// </summary>
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

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
        /// Danh sách thẻ tìm kiém theo môn học
        /// </summary>
        public List<String> SearchTags { get; set; }

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
