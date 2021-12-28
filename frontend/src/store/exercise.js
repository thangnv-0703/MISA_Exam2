import Resource from '../script/resource';
import axios from 'axios';

const initialExercise = {
  ExerciseId: null,
  ExerciseName: '',
  ExerciseImage: null,

  GradeId: null,
  GradeName: '',
  GradeCode: '',

  SubjectId: null,
  SubjectName: '',
  SubjectCode: '',

  TopicName: '',
  TopicId: null,

  SearchTags: [],
  Questions: [],
  QuestionCount: 0,
  Status: 0,
};

const moduleExercise = {
  state: () => ({
    exerciseData: initialExercise,
    exercises: [],
  }),
  getters: {
    questionList: (state) => state.exerciseData.Questions,
    exerciseList: (state) => state.exercises,
  },
  actions: {
    /**
     * Gọi api dữ liệu bài tập
     * @param {*} context
     */
    getExercises(context) {
      axios.get(Resource.api.exercise).then((response) => {
        context.commit('SET_EXERCISE_LIST', response.data);
      });
    },
    getExerciseById({ state, commit }) {
      axios
        .get(Resource.api.exercise + '/' + state.exerciseData.ExerciseId)
        .then((response) => {
          commit('SET_EXERCISE', response.data);
        });
    },
  },
  mutations: {
    /**
     * Gắn lại giá trị khối lớp
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_EXERCISE_GRADE(state, data) {
      for (const element in data) {
        state.exerciseData[element] = data[element];
      }
    },
    /**
     * Gắn lại giá trị môn học
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_EXERCISE_SUBJECT(state, data) {
      for (const element in data) {
        state.exerciseData[element] = data[element];
      }
    },
    /**
     * Gắn lại giá trị tên bài tập
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_EXERCISE_NAME(state, data) {
      state.exerciseData.ExerciseName = data;
    },

    /**
     * Gắn lại giá trị Id bài tập
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_EXERCISE_EXERCISEID(state, data) {
      state.exerciseData.ExerciseId = data;
    },

    /**
     * Gắn lại giá trị trạng thái
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_EXERCISE_STATUS(state, data) {
      state.exerciseData.Status = data;
    },

    /**
     * Gắn lại giá trị thẻ tìm kiếm
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_EXERCISE_SEARCHTAGS(state, data) {
      state.exerciseData.SearchTags = data;
    },

    /**
     * Gắn lại dữ liệu Id chủ đề
     * Author: NVTHANG (23/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_EXERCISE_TOPICID(state, data) {
      state.exerciseData.TopicId = data;
    },

    /**
     * Gắn lại dữ liệu ảnh bài tập
     * Author: NVTHANG (23/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_EXERCISE_IMAGE(state, data) {
      state.exerciseData.ExerciseImage = data;
    },

    /**
     * Reset lại giá trị exercise
     * Author: NVTHANG (23/12/2021)
     * @param {*} state
     * @param {*} data
     */
    RESET_EXERCISE(state) {
      state.exerciseData = {
        ExerciseId: null,
        ExerciseName: '',
        ExerciseImage: null,

        GradeId: null,
        GradeName: '',
        GradeCode: '',

        SubjectId: null,
        SubjectName: '',
        SubjectCode: '',

        TopicName: '',
        TopicId: null,

        SearchTags: [],
        Questions: [],
        Status: 0,
      };
    },

    /**
     * Thêm câu hỏi vào bài tập
     * Author: NVTHANG (18/12/2021)
     * @param {*} state
     * @param {*} data
     */
    PUSH_EXERCISE_QUESTION(state, data) {
      state.exerciseData.Questions.push(data);
      state.exerciseData.QuestionCount = state.exerciseData.Questions.length;
    },

    /**
     * Cập nhật lại dữ liệu câu hỏi
     * Author: NVTHANG (20/12/2021)
     * @param {*} state
     * @param {*} data
     */
    UPDATE_EXERCISE_QUESTION(state, data) {
      state.exerciseData.Questions.splice(data.index, 1, data.question);
      state.exerciseData.QuestionCount = state.exerciseData.Questions.length;
    },

    /**
     * Cập nhật lại số thứ tự câu hỏi
     * Author: NVTHANG (20/12/2021)
     * @param {*} state
     * @param {*} data
     */
    UPDATE_EXERCISE_QUESTION_NUMBER(state) {
      state.exerciseData.Questions.forEach((question, index) => {
        question.QuestionNumber = index + 1;
      });
      state.exerciseData.QuestionCount = state.exerciseData.Questions.length;
    },

    /**
     * Gắn dữ liệu danh sách bài tập
     * Author: NVTHANG (19/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_EXERCISE_LIST(state, data) {
      state.exercises = [...data];
    },

    /**
     * Reset dữ liệu list bài tập
     * Author: NVTHANG (19/12/2021)
     * @param {*} state
     * @param {*} data
     */
    RESET_EXERCISE_LIST(state) {
      state.exercises = [];
    },

    /**
     * Xóa dữ liệu bài tập
     * Author: NVTHANG (20/12/2021)
     * @param {*} state
     * @param {*} data
     */
    DELETE_EXERCISE_BY_ID(state, data) {
      state.exercises = state.exercises.filter(
        (exercise) => exercise.ExerciseId !== data,
      );
    },

    DELETE_QUESTION(state, data) {
      state.exerciseData.Questions.splice(data, 1);
      state.exerciseData.QuestionCount = state.exerciseData.Questions.length;
    },

    /**
     * Gắn dữ liệu bài tập
     * Author: NVTHANG (19/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_EXERCISE(state, data) {
      state.exerciseData = data;
    },
  },
};

export default moduleExercise;
