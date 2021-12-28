// const initialAnswers = [
//   {
//     AnswerContent: '',
//     AnswerImgUrl: '',
//     AnswerCorrect: false,
//   },
// ];

const initialQuestion = {
  QuestionNumber: 1,
  QuestionType: null,
  QuestionContent: '',
  QuestionAnswerContent: '',
  QuestionImage: null,
  Answers: [],
};

const moduleQuestion = {
  state: () => ({
    questionData: initialQuestion,
    questionDialog: {
      isShowDialog: false,
      questionType: null,
      questionMode: 1,
    },
    isShowAnswerDialog: false,
  }),

  mutations: {
    /**
     * Gắn dữ liệu câu hỏi
     * Author: NVTHANG (19/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_QUESTION(state, data) {
      state.questionData = data;
    },

    /**
     * Gắn lại giá trị số thứ tự câu hỏi
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_QUESTION_NUMBER(state, data) {
      state.questionData.QuestionNumber = data;
    },

    /**
     * Gắn lại giá trị kiểu câu hỏi
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_QUESTION_TYPE(state, data) {
      state.questionData.QuestionType = data;
    },

    /**
     * Gắn lại giá trị nội dung câu hỏi
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_QUESTION_CONTENT(state, data) {
      state.questionData.QuestionContent = data;
    },

    /**
     * Gắn lại giá trị ảnh câu hỏi
     * Author: NVTHANG (24/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_QUESTION_IMAGE(state, data) {
      state.questionData.QuestionImage = data;
    },

    /**
     * Gắn lại giá trị lời giải bài tập
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_QUESTION_ANSWER_CONTENT(state, data) {
      state.questionData.QuestionAnswerContent = data;
    },

    /**
     * Gắn lại giá trị danh sách đáp án
     * Author: NVTHANG (15/12/2021)
     * @param {*} state
     * @param {*} data
     */
    SET_ANSWERS(state, data) {
      state.questionData.Answers = data;
    },

    /**
     * reset lại state của phần trả lời
     * Author: NVTHANG (17/12/2021)
     * @param {*} state
     */
    RESET_ANSWER(state) {
      state.questionData.Answers = [];
    },

    /**
     * reset lại state của câu hỏi
     * Author: NVTHANG (17/12/2021)
     * @param {*} state
     */
    RESET_QUESTION(state) {
      state.questionData = {
        QuestionNumber: 0,
        QuestionType: null,
        QuestionContent: '',
        QuestionAnswerContent: '',
        QuestionImage: null,
        Answers: [],
      };
    },

    /**
     * Hiện thị dialog câu hỏi
     * Author: NVTHANG (19/12/2021)
     * @param {*} state
     * @param {*} data
     */
    OPEN_QUESTION_DIALOG(state, data) {
      state.questionDialog.isShowDialog = true;
      state.questionDialog.questionType = data.questionType;
      state.questionDialog.questionMode = data.questionMode;
    },

    /**
     * Ẩn dialog câu hỏi
     * @param {*} state
     * @param {*} data
     */
    CLOSE_QUESTION_DIALOG(state) {
      state.questionDialog = {
        isShowDialog: false,
        questionType: null,
        questionMode: 1,
      };
    },

    /**
     * Hàm hiện thị dialog lời giải
     * Author: NVTHANG(15/12/2021)
     * @param {*} state
     */
    OPEN_ANSWER_DIALOG(state) {
      state.isShowAnswerDialog = true;
    },

    /**
     * Hàm ẩn dialog lời giải
     * Author: NVTHANG(21/12/2021)
     * @param {*} state
     */
    CLOSE_ANSWER_DIALOG(state) {
      state.isShowAnswerDialog = false;
    },
  },
};

export default moduleQuestion;
