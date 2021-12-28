import Resource from '../script/resource';
import axios from 'axios';

const moduleTopic = {
  state: () => ({
    topics: [],
    initialTopic: [],
  }),
  actions: {
    /**
     * Gọi api dữ liệu khối lớp
     * @param {*} context
     */
    getTopics(context) {
      axios.get(Resource.api.topic).then((response) => {
        context.commit('SET_TOPICS', response.data);
      });
    },
  },
  mutations: {
    /**
     * Gán dữ liệu chủ đề
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_TOPICS(state, data) {
      let newValue = [];
      data.forEach((element) => {
        newValue.push({
          id: element.TopicId,
          label: element.TopicName,
          subjectId: element.SubjectId,
        });
      });
      state.topics = [...newValue];
      state.initialTopic = [...newValue];
    },

    /**
     * Cập nhật lại dữ liệu danh sách chủ đề theo môn học
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    UPDATE_TOPICS: function (state, data) {
      if (data === -1) {
        state.topics = state.initialTopic;
      } else {
        state.topics = state.initialTopic.filter((topic) => {
          return topic.subjectId === data;
        });
      }
    },
  },
};

export default moduleTopic;
