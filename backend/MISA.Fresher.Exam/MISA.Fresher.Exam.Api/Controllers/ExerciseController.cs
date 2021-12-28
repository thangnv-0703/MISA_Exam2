using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Exam.Core.Entities;
using MISA.Fresher.Exam.Core.Interfaces.Repository;
using MISA.Fresher.Exam.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.Exam.Api.Controllers
{
    public class ExerciseController : MISAController<Exercise>
    {
        IExerciseRepository _exerciseRepository;
        public ExerciseController(IBaseService<Exercise> baseService, IExerciseRepository exerciseRepository) : base(baseService, exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        /// <summary>
        /// Lọc dữ liệu theo tiêu chí
        /// </summary>
        /// <returns>
        /// 200 - thành công
        /// 204 - không có dữ liệu
        /// 400 - lỗi đầu vào
        /// 500 - lỗi server side
        /// </returns>
        /// createdBy: NVThang (18/11)
        [HttpGet("filter")]
        public IActionResult Get(string searchText, string subjectId, string gradeId, string topicId, int status, int pageSize, int pageIndex)
        {
            try
            {
                // Lấy dữ liệu từ database
                var employee = _exerciseRepository.GetExerciseePaging(searchText, subjectId, gradeId, topicId, status, pageSize, pageIndex);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
