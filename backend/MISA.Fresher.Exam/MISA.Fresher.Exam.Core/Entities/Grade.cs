using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Exam.Core.Entities
{
    public class Grade
    {
        #region Constructor
        public Grade()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// Id khối lớp
        /// </summary>
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string GradeId { get; set; }
        /// <summary>
        /// Tên khối lớp
        /// </summary>
        public string GradeName { get; set; }
        /// <summary>
        /// Mã khối lớp
        /// </summary>
        public string GradeCode { get; set; }

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
