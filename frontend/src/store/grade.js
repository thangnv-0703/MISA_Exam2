import Resource from '../script/resource';
import axios from 'axios';

const moduleGrade = {
  state: () => ({
    grades: [],
  }),
  getters: {
    /**
     * Lấy danh sách khối lớp
     * Author: NVTHANG (31/12/2021)
     * @param {*} state
     * @returns
     */
    getGradeList: (state) => state.grades,
  },
  actions: {
    /**
     * Gọi api dữ liệu khối lớp
     * @param {*} context
     */
    async getGrades(context) {
      await axios.get(Resource.api.grade).then((response) => {
        context.commit('SET_GRADES', response.data);
      });
    },
  },
  mutations: {
    /**
     * Gán dữ liệu khối lớp
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_GRADES(state, data) {
      let newValue = [];
      data.forEach((element) => {
        newValue.push({
          id: element.GradeId,
          label: element.GradeName,
          code: element.GradeCode,
        });
      });
      state.grades = [...newValue];
    },
  },
};

export default moduleGrade;
