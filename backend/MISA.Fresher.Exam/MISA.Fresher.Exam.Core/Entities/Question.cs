using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Exam.Core.Entities
{
    public class Question
    {
        #region Constructor
        public Question()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// Số thứ tự câu hỏi
        /// </summary>
        public string QuestionNumber { get; set; }
        /// <summary>
        /// Kiểu câu hỏi
        /// </summary>
        public int QuestionType { get; set; }
        /// <summary>
        /// Nội dung câu hỏi
        /// </summary>
        public string QuestionContent { get; set; }
        /// <summary>
        /// Ảnh của câu hỏi
        /// </summary>
        public string QuestionImage { get; set; }
        /// <summary>
        /// Danh sách phương án
        /// </summary>

        public List<Answer> Answers { get; set; }
        #endregion
    }
}
