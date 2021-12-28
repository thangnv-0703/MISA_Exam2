using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Exam.Core.Entities
{
    public class Answer
    {
        public Answer()
        {

        }
        /// <summary>
        /// Nội dung  phương án
        /// </summary>
        public string AnswerContent { get; set; }
        /// <summary>
        /// Url ảnh  phương án
        /// </summary>
        public string AnswerImgUrl { get; set; }
        /// <summary>
        /// Đáp án
        /// </summary>
        public bool AnswerCorrect { get; set; }
    }
}
