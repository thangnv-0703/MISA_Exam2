using MISA.Fresher.Exam.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Exam.Core.Interfaces.Repository
{
    public interface IExerciseRepository: IBaseRepository<Exercise>
    {
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
        public object GetExerciseePaging(string searchText, string subjectId, string gradeId, string topicId, int status, int pageSize, int pageIndex);
    }
}
