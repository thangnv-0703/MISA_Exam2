import Resource from '../script/resource';
import axios from 'axios';

const moduleSubject = {
  state: () => ({
    subjects: [],
    searchTags: [],
  }),
  getters: {
    /**
     * Lấy danh sách môn học
     * Author: NVTHANG (31/12/2021)
     * @param {*} state
     * @returns
     */
    getSubjectList: (state) => state.subjects,
    /**
     * Lấy danh sách thẻ tìm kiếm
     * Author: NVTHANG (31/12/2021)
     * @param {*} state
     * @returns
     */
    getSearchTags: (state) => state.searchTags,
  },
  actions: {
    /**
     * Gọi api dữ liệu khối lớp
     * @param {*} context
     */
    async getSubjects(context) {
      await axios.get(Resource.api.subject).then((response) => {
        context.commit('SET_SUBJECTS', response.data);
      });
      context.commit('SET_SEARCH_TAGS', -1);
    },
  },
  mutations: {
    /**
     * Gán dữ liệu môn học
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_SUBJECTS(state, data) {
      let newValue = [];
      data.forEach((element) => {
        newValue.push({
          id: element.SubjectId,
          label: element.SubjectName,
          code: element.SubjectCode,
          searchTags: element.SearchTags,
        });
      });
      state.subjects = [...newValue];
    },

    /**
     * Xét lại giá trị bộ thẻ tìm kiếm
     * Author: NVTHANG (22/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_SEARCH_TAGS(state, data) {
      if (state.subjects.length > 0) {
        let newSearchTags = [];
        if (data === -1) {
          state.subjects.forEach((element) => {
            newSearchTags = newSearchTags.concat(
              element.searchTags.slice(0, 2),
            );
          });
        } else {
          newSearchTags = state.subjects.filter(
            (element) => element.id === data,
          )[0].searchTags;
        }
        state.searchTags = [...newSearchTags];
      }
    },
  },
};

export default moduleSubject;
